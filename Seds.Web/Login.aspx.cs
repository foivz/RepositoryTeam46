using Seds.DataAccess;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;

namespace Seds.Web
{
    public partial class Login : System.Web.UI.Page
    {
        private const string _cookieName = "fc";
        public const string _sessionName = "CurrentUser";
        private const string _loginUrl = "~/Login.aspx";
        private static byte[] _encryptionKey = Converter.ToBytes("FA68FD569129732D10ED12683728931145098B22CEB0EEA25D56FE17CAE5A2FA");

        private SedsRepositories _repository = null;

        public SedsRepositories Repository
        {
            get
            {
                if (_repository == null)
                {
                    _repository = new SedsRepositories();
                }

                return _repository;
            }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            btnLogin.Click += btnLogin_Click;
        }

        void btnLogin_Click(object sender, EventArgs e)
        {
            var validationResult = Authorize(txtUserName.Text.Trim(), txtPassword.Text.Trim());

            switch (validationResult)
            {
                case AuthorizationResult.OK:
                    string url = HttpUtility.UrlDecode(Request.QueryString["ReturnUrl"] ?? ResolveUrl("~/home.aspx"));
                    Response.Redirect(url, false);
                    return;
                case AuthorizationResult.InvalidUserNameOrPassword:
                    MessageBox.ShowError("Neispravno ime ili lozinka");
                    break;
                case AuthorizationResult.UserNameEmpty:
                    MessageBox.ShowError("Upišite korisničko ime");
                    break;
                case AuthorizationResult.UserPasswordEmpty:
                    MessageBox.ShowError("Upišite lozinku");
                    break;
                case AuthorizationResult.Error:
                default:
                    MessageBox.ShowError("Greška");
                    break;
            }
        }

        [Serializable]
        public enum AuthorizationResult
        {
            Error,
            OK,
            UserNameEmpty,
            UserPasswordEmpty,
            InvalidUserNameOrPassword
        }

        public static void Authenticate(bool allowAnnonymous = false)
        {
            if (!allowAnnonymous)
            {
                string userIdentifier = GetUserNameFromCookie();
                if (userIdentifier == null)
                {
                    string url = (HttpContext.Current.Handler as Page).ResolveUrl(string.Format(string.Format("{0}?{1}={2}", _loginUrl, "ReturnUrl", HttpUtility.UrlEncode(HttpContext.Current.Request.Url.ToString())), new object[0]));
                    HttpContext.Current.Response.Redirect(url, true);
                }
            }
        }

        public static string GetUserNameFromCookie()
        {
            string userName = null;

            string cookieValue = HttpContext.Current.Request.Cookies[_cookieName] != null ? HttpContext.Current.Request.Cookies[_cookieName].Value : null;
            
            if (!string.IsNullOrWhiteSpace(cookieValue))
            {
                string text = RijndaelCrypt.DecryptFromBase64UrlDecoded(cookieValue);
                if (!string.IsNullOrWhiteSpace(text))
                {
                    userName = text;
                }
            }

            return userName;
        }

        

        public AuthorizationResult Authorize(string userName, string password)
        {
            HttpContext current = HttpContext.Current;
            AuthorizationResult result;
            if (!string.IsNullOrWhiteSpace(userName))
            {
                if (!string.IsNullOrWhiteSpace(password))
                {
                    var user = Repository.Users.GetAll().FirstOrDefault(u => u.UserName == userName);

                    if (user != null && user.Id > 0)
                    {
                        if (UnixCrypt.ValidatePassword(password.Trim(), user.Password))
                        {
                            string cookieValue = RijndaelCrypt.EncryptToBase64UrlEncoded(user.UserName);
                            HttpContext.Current.Response.Cookies.Add(new HttpCookie(_cookieName, cookieValue));

                            result = AuthorizationResult.OK;
                        }
                        else
                        {
                            result = AuthorizationResult.InvalidUserNameOrPassword;
                        }
                    }
                    else
                    {
                        result = AuthorizationResult.InvalidUserNameOrPassword;
                    }
                }
                else
                {
                    result = AuthorizationResult.UserPasswordEmpty;
                }
            }
            else
            {
                result = AuthorizationResult.UserNameEmpty;
            }
            return result;
        }

        public static void Clear()
        {
            HttpContext.Current.Response.Cookies.Add(new HttpCookie(_cookieName, RijndaelCrypt.EncryptToBase64UrlEncoded(string.Empty)));
        }

        public string GetLogoutUrl()
        {
            return (HttpContext.Current.Handler as Page).ResolveUrl(_loginUrl);
        }

        #region Crypto

        public class RijndaelCrypt
        {
            public static string EncryptToBase64UrlEncoded(string clearText)
            {
                byte[] encryptionKey = _encryptionKey;
                return RijndaelCrypt.EncryptToBase64UrlEncoded(clearText, encryptionKey);
            }

            public static string EncryptToBase64UrlEncoded(string clearText, string hexKey)
            {
                string result;
                if (clearText == null)
                {
                    result = null;
                }
                else
                {
                    byte[] key = Converter.ToBytes(hexKey);
                    result = RijndaelCrypt.EncryptToBase64UrlEncoded(clearText, key);
                }
                return result;
            }
            public static string EncryptToBase64UrlEncoded(string clearText, byte[] key)
            {
                string result;
                if (clearText == null)
                {
                    result = null;
                }
                else
                {
                    string s = RijndaelCrypt.EncryptToBase64(clearText, key);
                    byte[] bytes = Encoding.UTF8.GetBytes(s);
                    string text = HttpServerUtility.UrlTokenEncode(bytes);
                    result = text;
                }
                return result;
            }
            public static string EncryptToBase64(string clearText)
            {
                return RijndaelCrypt.EncryptToBase64(clearText, _encryptionKey);
            }
            public static string EncryptToBase64(string clearText, byte[] key)
            {
                string result;
                if (clearText == null)
                {
                    result = null;
                }
                else
                {
                    byte[] inArray = RijndaelCrypt.Encrypt(clearText, key, null);
                    result = Convert.ToBase64String(inArray);
                }
                return result;
            }
            public static byte[] Encrypt(string clearText)
            {
                byte[] encryptionKey = _encryptionKey;
                return RijndaelCrypt.Encrypt(clearText, encryptionKey, null);
            }
            public static byte[] Encrypt(string clearText, byte[] key, byte[] IV = null)
            {
                byte[] result;
                if (clearText == null)
                {
                    result = null;
                }
                else
                {
                    byte[] bytes = Encoding.UTF8.GetBytes(clearText);
                    Rijndael rijndael = new RijndaelManaged
                    {
                        Mode = CipherMode.CBC,
                        Key = key
                    };
                    if (IV != null)
                    {
                        rijndael.IV = IV;
                    }
                    MemoryStream memoryStream = new MemoryStream();
                    ICryptoTransform transform = rijndael.CreateEncryptor();
                    CryptoStream cryptoStream = new CryptoStream(memoryStream, transform, CryptoStreamMode.Write);
                    try
                    {
                        cryptoStream.Write(bytes, 0, bytes.Length);
                    }
                    finally
                    {
                        cryptoStream.FlushFinalBlock();
                        cryptoStream.Close();
                    }
                    byte[] array = memoryStream.ToArray();
                    byte[] iV = rijndael.IV;
                    byte[] array2 = new byte[array.Length + iV.Length];
                    Array.Copy(iV, 0, array2, 0, iV.Length);
                    Array.Copy(array, 0, array2, iV.Length, array.Length);
                    result = array2;
                }
                return result;
            }
            public static string DecryptFromBase64UrlDecoded(string encrypted)
            {
                byte[] encryptionKey = _encryptionKey;
                return RijndaelCrypt.DecryptFromBase64UrlDecoded(encrypted, encryptionKey);
            }
            public static string DecryptFromBase64UrlDecoded(string encrypted, string hexKey)
            {
                string result;
                if (encrypted == null)
                {
                    result = null;
                }
                else
                {
                    byte[] key = Converter.ToBytes(hexKey);
                    result = RijndaelCrypt.DecryptFromBase64UrlDecoded(encrypted, key);
                }
                return result;
            }
            public static string DecryptFromBase64UrlDecoded(string encrypted, byte[] key)
            {
                string result;
                if (encrypted == null)
                {
                    result = null;
                }
                else
                {
                    byte[] bytes = HttpServerUtility.UrlTokenDecode(encrypted);
                    string @string = Encoding.UTF8.GetString(bytes);
                    string text = RijndaelCrypt.DecryptFromBase64(@string, key);
                    result = text;
                }
                return result;
            }
            public static string DecryptFromBase64(string encrypted)
            {
                byte[] encryptionKey = _encryptionKey;
                return RijndaelCrypt.DecryptFromBase64(encrypted, encryptionKey);
            }
            public static string DecryptFromBase64(string encrypted, byte[] key)
            {
                byte[] encryptedBytesWithIv = Convert.FromBase64String(encrypted);
                return RijndaelCrypt.Decrypt(encryptedBytesWithIv, key, null);
            }
            public static string Decrypt(byte[] encryptedBytesWithIv)
            {
                byte[] encryptionKey = _encryptionKey;
                return RijndaelCrypt.Decrypt(encryptedBytesWithIv, encryptionKey, null);
            }
            public static string Decrypt(byte[] encryptedBytesWithIv, byte[] key, byte[] IV = null)
            {
                string result;
                if (encryptedBytesWithIv == null)
                {
                    result = null;
                }
                else
                {
                    byte[] array = IV;
                    if (array == null)
                    {
                        array = new byte[16];
                        Array.Copy(encryptedBytesWithIv, 0, array, 0, 16);
                    }
                    byte[] array2 = new byte[encryptedBytesWithIv.Length - array.Length];
                    Array.Copy(encryptedBytesWithIv, 16, array2, 0, array2.Length);
                    MemoryStream memoryStream = new MemoryStream();
                    Rijndael rijndael = new RijndaelManaged
                    {
                        Mode = CipherMode.CBC,
                        IV = array
                    };
                    ICryptoTransform transform = rijndael.CreateDecryptor(key, rijndael.IV);
                    CryptoStream cryptoStream = new CryptoStream(memoryStream, transform, CryptoStreamMode.Write);
                    try
                    {
                        cryptoStream.Write(array2, 0, array2.Length);
                    }
                    finally
                    {
                        cryptoStream.FlushFinalBlock();
                        cryptoStream.Close();
                    }
                    byte[] bytes = memoryStream.ToArray();
                    result = Encoding.UTF8.GetString(bytes);
                }
                return result;
            }
        }

        public sealed class UnixCrypt
        {
            private static readonly string m_encryptionSaltCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789./";
            private static readonly uint[] m_saltTranslation = new uint[]
			{
				0u,
				0u,
				0u,
				0u,
				0u,
				0u,
				0u,
				0u,
				0u,
				0u,
				0u,
				0u,
				0u,
				0u,
				0u,
				0u,
				0u,
				0u,
				0u,
				0u,
				0u,
				0u,
				0u,
				0u,
				0u,
				0u,
				0u,
				0u,
				0u,
				0u,
				0u,
				0u,
				0u,
				0u,
				0u,
				0u,
				0u,
				0u,
				0u,
				0u,
				0u,
				0u,
				0u,
				0u,
				0u,
				0u,
				0u,
				1u,
				2u,
				3u,
				4u,
				5u,
				6u,
				7u,
				8u,
				9u,
				10u,
				11u,
				5u,
				6u,
				7u,
				8u,
				9u,
				10u,
				11u,
				12u,
				13u,
				14u,
				15u,
				16u,
				17u,
				18u,
				19u,
				20u,
				21u,
				22u,
				23u,
				24u,
				25u,
				26u,
				27u,
				28u,
				29u,
				30u,
				31u,
				32u,
				33u,
				34u,
				35u,
				36u,
				37u,
				32u,
				33u,
				34u,
				35u,
				36u,
				37u,
				38u,
				39u,
				40u,
				41u,
				42u,
				43u,
				44u,
				45u,
				46u,
				47u,
				48u,
				49u,
				50u,
				51u,
				52u,
				53u,
				54u,
				55u,
				56u,
				57u,
				58u,
				59u,
				60u,
				61u,
				62u,
				63u,
				0u,
				0u,
				0u,
				0u,
				0u
			};
            private static readonly bool[] m_shifts = new bool[]
			{
				false,
				false,
				true,
				true,
				true,
				true,
				true,
				true,
				false,
				true,
				true,
				true,
				true,
				true,
				true,
				false
			};
            private static readonly uint[,] m_skb = new uint[,]
			{
				
				{
					0u,
					16u,
					536870912u,
					536870928u,
					65536u,
					65552u,
					536936448u,
					536936464u,
					2048u,
					2064u,
					536872960u,
					536872976u,
					67584u,
					67600u,
					536938496u,
					536938512u,
					32u,
					48u,
					536870944u,
					536870960u,
					65568u,
					65584u,
					536936480u,
					536936496u,
					2080u,
					2096u,
					536872992u,
					536873008u,
					67616u,
					67632u,
					536938528u,
					536938544u,
					524288u,
					524304u,
					537395200u,
					537395216u,
					589824u,
					589840u,
					537460736u,
					537460752u,
					526336u,
					526352u,
					537397248u,
					537397264u,
					591872u,
					591888u,
					537462784u,
					537462800u,
					524320u,
					524336u,
					537395232u,
					537395248u,
					589856u,
					589872u,
					537460768u,
					537460784u,
					526368u,
					526384u,
					537397280u,
					537397296u,
					591904u,
					591920u,
					537462816u,
					537462832u
				},
				
				{
					0u,
					33554432u,
					8192u,
					33562624u,
					2097152u,
					35651584u,
					2105344u,
					35659776u,
					4u,
					33554436u,
					8196u,
					33562628u,
					2097156u,
					35651588u,
					2105348u,
					35659780u,
					1024u,
					33555456u,
					9216u,
					33563648u,
					2098176u,
					35652608u,
					2106368u,
					35660800u,
					1028u,
					33555460u,
					9220u,
					33563652u,
					2098180u,
					35652612u,
					2106372u,
					35660804u,
					268435456u,
					301989888u,
					268443648u,
					301998080u,
					270532608u,
					304087040u,
					270540800u,
					304095232u,
					268435460u,
					301989892u,
					268443652u,
					301998084u,
					270532612u,
					304087044u,
					270540804u,
					304095236u,
					268436480u,
					301990912u,
					268444672u,
					301999104u,
					270533632u,
					304088064u,
					270541824u,
					304096256u,
					268436484u,
					301990916u,
					268444676u,
					301999108u,
					270533636u,
					304088068u,
					270541828u,
					304096260u
				},
				
				{
					0u,
					1u,
					262144u,
					262145u,
					16777216u,
					16777217u,
					17039360u,
					17039361u,
					2u,
					3u,
					262146u,
					262147u,
					16777218u,
					16777219u,
					17039362u,
					17039363u,
					512u,
					513u,
					262656u,
					262657u,
					16777728u,
					16777729u,
					17039872u,
					17039873u,
					514u,
					515u,
					262658u,
					262659u,
					16777730u,
					16777731u,
					17039874u,
					17039875u,
					134217728u,
					134217729u,
					134479872u,
					134479873u,
					150994944u,
					150994945u,
					151257088u,
					151257089u,
					134217730u,
					134217731u,
					134479874u,
					134479875u,
					150994946u,
					150994947u,
					151257090u,
					151257091u,
					134218240u,
					134218241u,
					134480384u,
					134480385u,
					150995456u,
					150995457u,
					151257600u,
					151257601u,
					134218242u,
					134218243u,
					134480386u,
					134480387u,
					150995458u,
					150995459u,
					151257602u,
					151257603u
				},
				
				{
					0u,
					1048576u,
					256u,
					1048832u,
					8u,
					1048584u,
					264u,
					1048840u,
					4096u,
					1052672u,
					4352u,
					1052928u,
					4104u,
					1052680u,
					4360u,
					1052936u,
					67108864u,
					68157440u,
					67109120u,
					68157696u,
					67108872u,
					68157448u,
					67109128u,
					68157704u,
					67112960u,
					68161536u,
					67113216u,
					68161792u,
					67112968u,
					68161544u,
					67113224u,
					68161800u,
					131072u,
					1179648u,
					131328u,
					1179904u,
					131080u,
					1179656u,
					131336u,
					1179912u,
					135168u,
					1183744u,
					135424u,
					1184000u,
					135176u,
					1183752u,
					135432u,
					1184008u,
					67239936u,
					68288512u,
					67240192u,
					68288768u,
					67239944u,
					68288520u,
					67240200u,
					68288776u,
					67244032u,
					68292608u,
					67244288u,
					68292864u,
					67244040u,
					68292616u,
					67244296u,
					68292872u
				},
				
				{
					0u,
					268435456u,
					65536u,
					268500992u,
					4u,
					268435460u,
					65540u,
					268500996u,
					536870912u,
					805306368u,
					536936448u,
					805371904u,
					536870916u,
					805306372u,
					536936452u,
					805371908u,
					1048576u,
					269484032u,
					1114112u,
					269549568u,
					1048580u,
					269484036u,
					1114116u,
					269549572u,
					537919488u,
					806354944u,
					537985024u,
					806420480u,
					537919492u,
					806354948u,
					537985028u,
					806420484u,
					4096u,
					268439552u,
					69632u,
					268505088u,
					4100u,
					268439556u,
					69636u,
					268505092u,
					536875008u,
					805310464u,
					536940544u,
					805376000u,
					536875012u,
					805310468u,
					536940548u,
					805376004u,
					1052672u,
					269488128u,
					1118208u,
					269553664u,
					1052676u,
					269488132u,
					1118212u,
					269553668u,
					537923584u,
					806359040u,
					537989120u,
					806424576u,
					537923588u,
					806359044u,
					537989124u,
					806424580u
				},
				
				{
					0u,
					134217728u,
					8u,
					134217736u,
					1024u,
					134218752u,
					1032u,
					134218760u,
					131072u,
					134348800u,
					131080u,
					134348808u,
					132096u,
					134349824u,
					132104u,
					134349832u,
					1u,
					134217729u,
					9u,
					134217737u,
					1025u,
					134218753u,
					1033u,
					134218761u,
					131073u,
					134348801u,
					131081u,
					134348809u,
					132097u,
					134349825u,
					132105u,
					134349833u,
					33554432u,
					167772160u,
					33554440u,
					167772168u,
					33555456u,
					167773184u,
					33555464u,
					167773192u,
					33685504u,
					167903232u,
					33685512u,
					167903240u,
					33686528u,
					167904256u,
					33686536u,
					167904264u,
					33554433u,
					167772161u,
					33554441u,
					167772169u,
					33555457u,
					167773185u,
					33555465u,
					167773193u,
					33685505u,
					167903233u,
					33685513u,
					167903241u,
					33686529u,
					167904257u,
					33686537u,
					167904265u
				},
				
				{
					0u,
					256u,
					524288u,
					524544u,
					16777216u,
					16777472u,
					17301504u,
					17301760u,
					16u,
					272u,
					524304u,
					524560u,
					16777232u,
					16777488u,
					17301520u,
					17301776u,
					2097152u,
					2097408u,
					2621440u,
					2621696u,
					18874368u,
					18874624u,
					19398656u,
					19398912u,
					2097168u,
					2097424u,
					2621456u,
					2621712u,
					18874384u,
					18874640u,
					19398672u,
					19398928u,
					512u,
					768u,
					524800u,
					525056u,
					16777728u,
					16777984u,
					17302016u,
					17302272u,
					528u,
					784u,
					524816u,
					525072u,
					16777744u,
					16778000u,
					17302032u,
					17302288u,
					2097664u,
					2097920u,
					2621952u,
					2622208u,
					18874880u,
					18875136u,
					19399168u,
					19399424u,
					2097680u,
					2097936u,
					2621968u,
					2622224u,
					18874896u,
					18875152u,
					19399184u,
					19399440u
				},
				
				{
					0u,
					67108864u,
					262144u,
					67371008u,
					2u,
					67108866u,
					262146u,
					67371010u,
					8192u,
					67117056u,
					270336u,
					67379200u,
					8194u,
					67117058u,
					270338u,
					67379202u,
					32u,
					67108896u,
					262176u,
					67371040u,
					34u,
					67108898u,
					262178u,
					67371042u,
					8224u,
					67117088u,
					270368u,
					67379232u,
					8226u,
					67117090u,
					270370u,
					67379234u,
					2048u,
					67110912u,
					264192u,
					67373056u,
					2050u,
					67110914u,
					264194u,
					67373058u,
					10240u,
					67119104u,
					272384u,
					67381248u,
					10242u,
					67119106u,
					272386u,
					67381250u,
					2080u,
					67110944u,
					264224u,
					67373088u,
					2082u,
					67110946u,
					264226u,
					67373090u,
					10272u,
					67119136u,
					272416u,
					67381280u,
					10274u,
					67119138u,
					272418u,
					67381282u
				}
			};
            private static readonly uint[,] m_SPTranslationTable = new uint[,]
			{
				
				{
					8520192u,
					131072u,
					2155872256u,
					2156003840u,
					8388608u,
					2147615232u,
					2147614720u,
					2155872256u,
					2147615232u,
					8520192u,
					8519680u,
					2147484160u,
					2155872768u,
					8388608u,
					0u,
					2147614720u,
					131072u,
					2147483648u,
					8389120u,
					131584u,
					2156003840u,
					8519680u,
					2147484160u,
					8389120u,
					2147483648u,
					512u,
					131584u,
					2156003328u,
					512u,
					2155872768u,
					2156003328u,
					0u,
					0u,
					2156003840u,
					8389120u,
					2147614720u,
					8520192u,
					131072u,
					2147484160u,
					8389120u,
					2156003328u,
					512u,
					131584u,
					2155872256u,
					2147615232u,
					2147483648u,
					2155872256u,
					8519680u,
					2156003840u,
					131584u,
					8519680u,
					2155872768u,
					8388608u,
					2147484160u,
					2147614720u,
					0u,
					131072u,
					8388608u,
					2155872768u,
					8520192u,
					2147483648u,
					2156003328u,
					512u,
					2147615232u
				},
				
				{
					268705796u,
					0u,
					270336u,
					268697600u,
					268435460u,
					8196u,
					268443648u,
					270336u,
					8192u,
					268697604u,
					4u,
					268443648u,
					262148u,
					268705792u,
					268697600u,
					4u,
					262144u,
					268443652u,
					268697604u,
					8192u,
					270340u,
					268435456u,
					0u,
					262148u,
					268443652u,
					270340u,
					268705792u,
					268435460u,
					268435456u,
					262144u,
					8196u,
					268705796u,
					262148u,
					268705792u,
					268443648u,
					270340u,
					268705796u,
					262148u,
					268435460u,
					0u,
					268435456u,
					8196u,
					262144u,
					268697604u,
					8192u,
					268435456u,
					270340u,
					268443652u,
					268705792u,
					8192u,
					0u,
					268435460u,
					4u,
					268705796u,
					270336u,
					268697600u,
					268697604u,
					262144u,
					8196u,
					268443648u,
					268443652u,
					4u,
					268697600u,
					270336u
				},
				
				{
					1090519040u,
					16842816u,
					64u,
					1090519104u,
					1073807360u,
					16777216u,
					1090519104u,
					65600u,
					16777280u,
					65536u,
					16842752u,
					1073741824u,
					1090584640u,
					1073741888u,
					1073741824u,
					1090584576u,
					0u,
					1073807360u,
					16842816u,
					64u,
					1073741888u,
					1090584640u,
					65536u,
					1090519040u,
					1090584576u,
					16777280u,
					1073807424u,
					16842752u,
					65600u,
					0u,
					16777216u,
					1073807424u,
					16842816u,
					64u,
					1073741824u,
					65536u,
					1073741888u,
					1073807360u,
					16842752u,
					1090519104u,
					0u,
					16842816u,
					65600u,
					1090584576u,
					1073807360u,
					16777216u,
					1090584640u,
					1073741824u,
					1073807424u,
					1090519040u,
					16777216u,
					1090584640u,
					65536u,
					16777280u,
					1090519104u,
					65600u,
					16777280u,
					0u,
					1090584576u,
					1073741888u,
					1090519040u,
					1073807424u,
					64u,
					16842752u
				},
				
				{
					1049602u,
					67109888u,
					2u,
					68158466u,
					0u,
					68157440u,
					67109890u,
					1048578u,
					68158464u,
					67108866u,
					67108864u,
					1026u,
					67108866u,
					1049602u,
					1048576u,
					67108864u,
					68157442u,
					1049600u,
					1024u,
					2u,
					1049600u,
					67109890u,
					68157440u,
					1024u,
					1026u,
					0u,
					1048578u,
					68158464u,
					67109888u,
					68157442u,
					68158466u,
					1048576u,
					68157442u,
					1026u,
					1048576u,
					67108866u,
					1049600u,
					67109888u,
					2u,
					68157440u,
					67109890u,
					0u,
					1024u,
					1048578u,
					0u,
					68157442u,
					68158464u,
					1024u,
					67108864u,
					68158466u,
					1049602u,
					1048576u,
					68158466u,
					2u,
					67109888u,
					1049602u,
					1048578u,
					1049600u,
					68157440u,
					67109890u,
					1026u,
					67108864u,
					67108866u,
					68158464u
				},
				
				{
					33554432u,
					16384u,
					256u,
					33571080u,
					33570824u,
					33554688u,
					16648u,
					33570816u,
					16384u,
					8u,
					33554440u,
					16640u,
					33554696u,
					33570824u,
					33571072u,
					0u,
					16640u,
					33554432u,
					16392u,
					264u,
					33554688u,
					16648u,
					0u,
					33554440u,
					8u,
					33554696u,
					33571080u,
					16392u,
					33570816u,
					256u,
					264u,
					33571072u,
					33571072u,
					33554696u,
					16392u,
					33570816u,
					16384u,
					8u,
					33554440u,
					33554688u,
					33554432u,
					16640u,
					33571080u,
					0u,
					16648u,
					33554432u,
					256u,
					16392u,
					33554696u,
					256u,
					0u,
					33571080u,
					33570824u,
					33571072u,
					264u,
					16384u,
					16640u,
					33570824u,
					33554688u,
					264u,
					8u,
					16648u,
					33570816u,
					33554440u
				},
				
				{
					536870928u,
					524304u,
					0u,
					537397248u,
					524304u,
					2048u,
					536872976u,
					524288u,
					2064u,
					537397264u,
					526336u,
					536870912u,
					536872960u,
					536870928u,
					537395200u,
					526352u,
					524288u,
					536872976u,
					537395216u,
					0u,
					2048u,
					16u,
					537397248u,
					537395216u,
					537397264u,
					537395200u,
					536870912u,
					2064u,
					16u,
					526336u,
					526352u,
					536872960u,
					2064u,
					536870912u,
					536872960u,
					526352u,
					537397248u,
					524304u,
					0u,
					536872960u,
					536870912u,
					2048u,
					537395216u,
					524288u,
					524304u,
					537397264u,
					526336u,
					16u,
					537397264u,
					526336u,
					524288u,
					536872976u,
					536870928u,
					537395200u,
					526352u,
					0u,
					2048u,
					536870928u,
					536872976u,
					537397248u,
					537395200u,
					2064u,
					16u,
					537395216u
				},
				
				{
					4096u,
					128u,
					4194432u,
					4194305u,
					4198529u,
					4097u,
					4224u,
					0u,
					4194304u,
					4194433u,
					129u,
					4198400u,
					1u,
					4198528u,
					4198400u,
					129u,
					4194433u,
					4096u,
					4097u,
					4198529u,
					0u,
					4194432u,
					4194305u,
					4224u,
					4198401u,
					4225u,
					4198528u,
					1u,
					4225u,
					4198401u,
					128u,
					4194304u,
					4225u,
					4198400u,
					4198401u,
					129u,
					4096u,
					128u,
					4194304u,
					4198401u,
					4194433u,
					4225u,
					4224u,
					0u,
					128u,
					4194305u,
					1u,
					4194432u,
					0u,
					4194433u,
					4194432u,
					4224u,
					129u,
					4096u,
					4198529u,
					4194304u,
					4198528u,
					1u,
					4097u,
					4198529u,
					4194305u,
					4198528u,
					4198400u,
					4097u
				},
				
				{
					136314912u,
					136347648u,
					32800u,
					0u,
					134250496u,
					2097184u,
					136314880u,
					136347680u,
					32u,
					134217728u,
					2129920u,
					32800u,
					2129952u,
					134250528u,
					134217760u,
					136314880u,
					32768u,
					2129952u,
					2097184u,
					134250496u,
					136347680u,
					134217760u,
					0u,
					2129920u,
					134217728u,
					2097152u,
					134250528u,
					136314912u,
					2097152u,
					32768u,
					136347648u,
					32u,
					2097152u,
					32768u,
					134217760u,
					136347680u,
					32800u,
					134217728u,
					0u,
					2129920u,
					136314912u,
					134250528u,
					134250496u,
					2097184u,
					136347648u,
					32u,
					2097184u,
					134250496u,
					136347680u,
					2097152u,
					136314880u,
					134217760u,
					2129920u,
					32800u,
					134250528u,
					136314880u,
					32u,
					136347648u,
					2129952u,
					0u,
					134217728u,
					136314912u,
					32768u,
					2129952u
				}
			};
            private static readonly uint[] m_characterConversionTable = new uint[]
			{
				46u,
				47u,
				48u,
				49u,
				50u,
				51u,
				52u,
				53u,
				54u,
				55u,
				56u,
				57u,
				65u,
				66u,
				67u,
				68u,
				69u,
				70u,
				71u,
				72u,
				73u,
				74u,
				75u,
				76u,
				77u,
				78u,
				79u,
				80u,
				81u,
				82u,
				83u,
				84u,
				85u,
				86u,
				87u,
				88u,
				89u,
				90u,
				97u,
				98u,
				99u,
				100u,
				101u,
				102u,
				103u,
				104u,
				105u,
				106u,
				107u,
				108u,
				109u,
				110u,
				111u,
				112u,
				113u,
				114u,
				115u,
				116u,
				117u,
				118u,
				119u,
				120u,
				121u,
				122u
			};
            private static readonly int m_desIterations = 16;
            private UnixCrypt()
            {
            }
            private static uint FourBytesToInt(byte[] inputBytes, uint offset)
            {
                int num = (int)(inputBytes[(int)((UIntPtr)(offset++))] & 255);
                num |= (int)(inputBytes[(int)((UIntPtr)(offset++))] & 255) << 8;
                num |= (int)(inputBytes[(int)((UIntPtr)(offset++))] & 255) << 16;
                return (uint)(num | (int)(inputBytes[(int)((UIntPtr)(offset++))] & 255) << 24);
            }
            private static void IntToFourBytes(uint inputInt, byte[] outputBytes, uint offset)
            {
                outputBytes[(int)((UIntPtr)(offset++))] = (byte)(inputInt & 255u);
                outputBytes[(int)((UIntPtr)(offset++))] = (byte)(inputInt >> 8 & 255u);
                outputBytes[(int)((UIntPtr)(offset++))] = (byte)(inputInt >> 16 & 255u);
                outputBytes[(int)((UIntPtr)(offset++))] = (byte)(inputInt >> 24 & 255u);
            }
            private static void PermOperation(uint firstInt, uint secondInt, uint thirdInt, uint fourthInt, uint[] operationResults)
            {
                uint num = (firstInt >> (int)thirdInt ^ secondInt) & fourthInt;
                firstInt ^= num << (int)thirdInt;
                secondInt ^= num;
                operationResults[0] = firstInt;
                operationResults[1] = secondInt;
            }
            private static uint HPermOperation(uint firstInt, int secondInt, uint thirdInt)
            {
                uint num = (firstInt << 16 - secondInt ^ firstInt) & thirdInt;
                return firstInt ^ num ^ num >> 16 - secondInt;
            }
            private static uint[] SetDESKey(byte[] encryptionKey)
            {
                uint[] array = new uint[UnixCrypt.m_desIterations * 2];
                uint num = UnixCrypt.FourBytesToInt(encryptionKey, 0u);
                uint num2 = UnixCrypt.FourBytesToInt(encryptionKey, 4u);
                uint[] array2 = new uint[2];
                UnixCrypt.PermOperation(num2, num, 4u, 252645135u, array2);
                num2 = array2[0];
                num = array2[1];
                num = UnixCrypt.HPermOperation(num, -2, 3435921408u);
                num2 = UnixCrypt.HPermOperation(num2, -2, 3435921408u);
                UnixCrypt.PermOperation(num2, num, 1u, 1431655765u, array2);
                num2 = array2[0];
                num = array2[1];
                UnixCrypt.PermOperation(num, num2, 8u, 16711935u, array2);
                num = array2[0];
                num2 = array2[1];
                UnixCrypt.PermOperation(num2, num, 1u, 1431655765u, array2);
                num2 = array2[0];
                num = array2[1];
                num2 = ((num2 & 255u) << 16 | (num2 & 65280u) | (num2 & 16711680u) >> 16 | (num & 4026531840u) >> 4);
                num &= 268435455u;
                uint num3 = 0u;
                for (int i = 0; i < UnixCrypt.m_desIterations; i++)
                {
                    bool flag = UnixCrypt.m_shifts[i];
                    if (flag)
                    {
                        num = (num >> 2 | num << 26);
                        num2 = (num2 >> 2 | num2 << 26);
                    }
                    else
                    {
                        num = (num >> 1 | num << 27);
                        num2 = (num2 >> 1 | num2 << 27);
                    }
                    num &= 268435455u;
                    num2 &= 268435455u;
                    uint num4 = UnixCrypt.m_skb[(int)((UIntPtr)0), (int)((UIntPtr)(num & 63u))] | UnixCrypt.m_skb[(int)((UIntPtr)1), (int)((UIntPtr)((num >> 6 & 3u) | (num >> 7 & 60u)))] | UnixCrypt.m_skb[(int)((UIntPtr)2), (int)((UIntPtr)((num >> 13 & 15u) | (num >> 14 & 48u)))] | UnixCrypt.m_skb[(int)((UIntPtr)3), (int)((UIntPtr)((num >> 20 & 1u) | (num >> 21 & 6u) | (num >> 22 & 56u)))];
                    uint num5 = UnixCrypt.m_skb[(int)((UIntPtr)4), (int)((UIntPtr)(num2 & 63u))] | UnixCrypt.m_skb[(int)((UIntPtr)5), (int)((UIntPtr)((num2 >> 7 & 3u) | (num2 >> 8 & 60u)))] | UnixCrypt.m_skb[(int)((UIntPtr)6), (int)((UIntPtr)(num2 >> 15 & 63u))] | UnixCrypt.m_skb[(int)((UIntPtr)7), (int)((UIntPtr)((num2 >> 21 & 15u) | (num2 >> 22 & 48u)))];
                    array[(int)((UIntPtr)(num3++))] = ((num5 << 16 | (num4 & 65535u)) & 4294967295u);
                    num4 = (num4 >> 16 | (num5 & 4294901760u));
                    num4 = (num4 << 4 | num4 >> 28);
                    array[(int)((UIntPtr)(num3++))] = (num4 & 4294967295u);
                }
                return array;
            }
            private static uint DEncrypt(uint left, uint right, uint scheduleIndex, uint firstSaltTranslator, uint secondSaltTranslator, uint[] schedule)
            {
                uint num = right ^ right >> 16;
                uint num2 = num & firstSaltTranslator;
                num &= secondSaltTranslator;
                num2 = (num2 ^ num2 << 16 ^ right ^ schedule[(int)((UIntPtr)scheduleIndex)]);
                uint num3 = num ^ num << 16 ^ right ^ schedule[(int)((UIntPtr)(scheduleIndex + 1u))];
                num3 = (num3 >> 4 | num3 << 28);
                left ^= (UnixCrypt.m_SPTranslationTable[(int)((UIntPtr)1), (int)((UIntPtr)(num3 & 63u))] | UnixCrypt.m_SPTranslationTable[(int)((UIntPtr)3), (int)((UIntPtr)(num3 >> 8 & 63u))] | UnixCrypt.m_SPTranslationTable[(int)((UIntPtr)5), (int)((UIntPtr)(num3 >> 16 & 63u))] | UnixCrypt.m_SPTranslationTable[(int)((UIntPtr)7), (int)((UIntPtr)(num3 >> 24 & 63u))] | UnixCrypt.m_SPTranslationTable[(int)((UIntPtr)0), (int)((UIntPtr)(num2 & 63u))] | UnixCrypt.m_SPTranslationTable[(int)((UIntPtr)2), (int)((UIntPtr)(num2 >> 8 & 63u))] | UnixCrypt.m_SPTranslationTable[(int)((UIntPtr)4), (int)((UIntPtr)(num2 >> 16 & 63u))] | UnixCrypt.m_SPTranslationTable[(int)((UIntPtr)6), (int)((UIntPtr)(num2 >> 24 & 63u))]);
                return left;
            }
            private static uint[] Body(uint[] schedule, uint firstSaltTranslator, uint secondSaltTranslator)
            {
                uint num = 0u;
                uint num2 = 0u;
                uint num4;
                for (int i = 0; i < 25; i++)
                {
                    uint num3 = 0u;
                    while ((ulong)num3 < (ulong)((long)(UnixCrypt.m_desIterations * 2)))
                    {
                        num = UnixCrypt.DEncrypt(num, num2, num3, firstSaltTranslator, secondSaltTranslator, schedule);
                        num2 = UnixCrypt.DEncrypt(num2, num, num3 + 2u, firstSaltTranslator, secondSaltTranslator, schedule);
                        num3 += 4u;
                    }
                    num4 = num;
                    num = num2;
                    num2 = num4;
                }
                num4 = num2;
                num2 = (num >> 1 | num << 31);
                num = (num4 >> 1 | num4 << 31);
                num &= 4294967295u;
                num2 &= 4294967295u;
                uint[] array = new uint[2];
                UnixCrypt.PermOperation(num2, num, 1u, 1431655765u, array);
                num2 = array[0];
                num = array[1];
                UnixCrypt.PermOperation(num, num2, 8u, 16711935u, array);
                num = array[0];
                num2 = array[1];
                UnixCrypt.PermOperation(num2, num, 2u, 858993459u, array);
                num2 = array[0];
                num = array[1];
                UnixCrypt.PermOperation(num, num2, 16u, 65535u, array);
                num = array[0];
                num2 = array[1];
                UnixCrypt.PermOperation(num2, num, 4u, 252645135u, array);
                num2 = array[0];
                num = array[1];
                return new uint[]
				{
					num,
					num2
				};
            }
            public static string Crypt(string textToEncrypt)
            {
                Random random = new Random();
                int length = UnixCrypt.m_encryptionSaltCharacters.Length;
                StringBuilder stringBuilder = new StringBuilder();
                for (int i = 0; i < 2; i++)
                {
                    int index = random.Next(length);
                    stringBuilder.Append(UnixCrypt.m_encryptionSaltCharacters[index]);
                }
                string encryptionSalt = stringBuilder.ToString();
                return UnixCrypt.Crypt(encryptionSalt, textToEncrypt);
            }
            public static string Crypt(string encryptionSalt, string textToEncrypt)
            {
                bool flag = encryptionSalt.Length < 2;
                if (flag)
                {
                    throw new ArgumentException("The encryptionSalt must be 2 characters big.");
                }
                char value = encryptionSalt[0];
                char value2 = encryptionSalt[1];
                StringBuilder stringBuilder = new StringBuilder("*************");
                stringBuilder[0] = value;
                stringBuilder[1] = value2;
                uint firstSaltTranslator = UnixCrypt.m_saltTranslation[(int)((UIntPtr)Convert.ToUInt32(value))];
                uint secondSaltTranslator = UnixCrypt.m_saltTranslation[(int)((UIntPtr)Convert.ToUInt32(value2))] << 4;
                byte[] array = new byte[8];
                int i = 0;
                while (i < array.Length && i < textToEncrypt.Length)
                {
                    int num = Convert.ToInt32(textToEncrypt[i]);
                    array[i] = (byte)(num << 1);
                    i++;
                }
                uint[] schedule = UnixCrypt.SetDESKey(array);
                uint[] array2 = UnixCrypt.Body(schedule, firstSaltTranslator, secondSaltTranslator);
                byte[] array3 = new byte[9];
                UnixCrypt.IntToFourBytes(array2[0], array3, 0u);
                UnixCrypt.IntToFourBytes(array2[1], array3, 4u);
                array3[8] = 0;
                uint num2 = 0u;
                uint num3 = 128u;
                for (i = 2; i < 13; i++)
                {
                    uint num4 = 0u;
                    for (int j = 0; j < 6; j++)
                    {
                        num4 <<= 1;
                        bool flag2 = ((uint)array3[(int)((UIntPtr)num2)] & num3) != 0u;
                        if (flag2)
                        {
                            num4 |= 1u;
                        }
                        num3 >>= 1;
                        bool flag3 = num3 == 0u;
                        if (flag3)
                        {
                            num2 += 1u;
                            num3 = 128u;
                        }
                    }
                    stringBuilder[i] = Convert.ToChar(UnixCrypt.m_characterConversionTable[(int)((UIntPtr)num4)]);
                }
                return stringBuilder.ToString();
            }
            public static bool ValidatePassword(string plainTextPassword, string encryptedPassword)
            {
                bool result = false;
                if (!string.IsNullOrWhiteSpace(plainTextPassword) && !string.IsNullOrWhiteSpace(encryptedPassword))
                {
                    string b = UnixCrypt.Crypt(encryptedPassword, plainTextPassword);
                    result = (encryptedPassword == b);
                }
                return result;
            }
        }

        #endregion

        public class Converter
        {
            public static byte[] ToBytes(string hex)
            {
                byte[] result;
                if (hex == null)
                {
                    result = null;
                }
                else
                {
                    byte[] array = new byte[hex.Length / 2];
                    for (int i = 0; i < hex.Length / 2; i++)
                    {
                        array[i] = Convert.ToByte(hex.Substring(i * 2, 2), 16);
                    }
                    result = array;
                }
                return result;
            }

            public static string ToHexString(byte[] bytes)
            {
                string text = "";
                for (int i = 0; i < bytes.Length; i++)
                {
                    byte value = bytes[i];
                    string text2 = Convert.ToString(value, 16).ToUpper();
                    if (text2.Length == 0)
                    {
                        text2 = "00";
                    }
                    if (text2.Length == 1)
                    {
                        text2 = string.Format("0{0}", text2);
                    }
                    text = string.Format("{0}{1}", text, text2);
                }
                return text;
            }
        }
    }
}