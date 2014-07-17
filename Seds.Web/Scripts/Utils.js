var SadaCore = {};
SadaCore.Debuging = false;
SadaCore.Audit = [];
SadaCore.Language = 'HR';
$(function () {

    try {
        SadaCore.Elements = {

            Body: theForm && $(theForm).parents ? $(theForm).parents("body:first") : $(":not('iframe')").find("body")
        };

        var body = SadaCore.Elements.Body;

        SadaCore.Ajax.Initialize();

        SadaCore.UI.SetFocusOnFirstElement();
    }
    catch (e) {
        SadaCore.HandleException(e.message, "SadaCore.ready()");
    }
});

SadaCore.Helper =
{
    IsObject: function (obj) {

        if (Object.prototype.toString.call(obj) !== "[object Object]") {
            return false;
        }
        var key;
        for (key in obj) { }
        return !key || Object.prototype.hasOwnProperty.call(obj, key);
    }
};

SadaCore.RegisterNameSpace = function (namespacePath) {

    var baseNS = window;
    var namespaceParts = namespacePath.split('.');

    for (var i = 0; i < namespaceParts.length; i++) {

        var currentPart = namespaceParts[i];
        var ns = baseNS[currentPart];

        if (!ns) {

            ns = baseNS[currentPart] = {};
        }
        baseNS = ns;
    }
};

SadaCore.MessageBoxControl = function (messageControlId, settings) {

    settings = $.extend({
        messageControlId: "dvMessageContainer",
        customHtmlEnabled: false,
        divPortalMessageMessageId: 'divMessageBoxSuccess',
        divPortalMessageErrorId: 'divMessageBoxError',
        divPortalMessageInfoId: 'divMessageBoxInfo',
        showMessageClass: "alert-success",
        showErrorClass: "alert-error",
        showInfoClass: "alert-info",
        labelSelector: "span",
        serviceUrl: "/UserControls/Message/MessageBoxService.asmx"
    }, settings || {});

    var _me = this;
    var _dvMessageContainer = null;
    var _divPortalMessageMessage = null;
    var _divPortalMessageError = null;
    var _divPortalMessageInfo = null;

    var _messageType = {
        Message: 1,
        Error: 2,
        Info: 3
    };

    if (typeof messageControlId != undefined) {
        settings = $.extend(settings, { messageControlId: messageControlId } || {});
    }

    var _Init = function () {

        _dvMessageContainer = $("div[id*='" + settings.messageControlId + "']");
        _divPortalMessageMessage = $("div[id*='" + settings.divPortalMessageMessageId + "']");
        _divPortalMessageError = $("div[id*='" + settings.divPortalMessageErrorId + "']");
        _divPortalMessageInfo = $("div[id*='" + settings.divPortalMessageInfoId + "']");

        _divPortalMessageInfo.removeClass(settings.showInfoClass).addClass(settings.showInfoClass);
        _divPortalMessageError.removeClass(settings.showErrorClass).addClass(settings.showErrorClass);
        _divPortalMessageMessage.removeClass(settings.showMessageClass).addClass(settings.showMessageClass);
    };

    this.Clear = function () {
        _dvMessageContainer.hide().find(settings.labelSelector).html("");
        _divPortalMessageInfo.hide().find(settings.labelSelector).html("");
        _divPortalMessageError.hide().find(settings.labelSelector).html("");
        _divPortalMessageMessage.hide().find(settings.labelSelector).html("");
    };

    this.ShowMessage = function (message, redirect, redirectUrl) {

        SetMessage(_messageType.Message, message, redirect, redirectUrl);
    };
    this.ShowInfo = function (message, redirect, redirectUrl) {

        SetMessage(_messageType.Info, message, redirect, redirectUrl);
    };
    this.ShowError = function (message, redirect, redirectUrl) {

        SetMessage(_messageType.Error, message, redirect, redirectUrl);
    };

    var SetMessage = function (messageType, message, redirect, redirectUrl) {

        _me.Clear();

        if (redirect === true) {

            redirectUrl = typeof redirectUrl != 'undefined' ? redirectUrl : window.location.href;

            if (redirectUrl.indexOf('#') > -1) {

                redirectUrl = redirectUrl.substring(0, redirectUrl.indexOf("#"));
            }

            if (SadaCore.MessageBoxControl.ShowLoaderOnRedirect === true) {

                SadaCore.Loader.Show();
            }

            SadaCore.Ajax(settings.serviceUrl + "/PutMessageInSession", { message: message, redirectUrl: redirectUrl, type: messageType }, function (response) {

                if (SadaCore.MessageBoxControl.ShowLoaderOnRedirect === true) {

                    SadaCore.HttpContext.Redirecting = true;
                }

                window.location.href = redirectUrl;
            });
        }
        else {

            switch (messageType) {
                case _messageType.Message:
                    _divPortalMessageMessage.find(settings.labelSelector).html(message);
                    _divPortalMessageMessage.show();
                    break;
                case _messageType.Error:
                    _divPortalMessageError.find(settings.labelSelector).html(message);
                    _divPortalMessageError.show();
                    break;
                case _messageType.Info:
                    _divPortalMessageInfo.find(settings.labelSelector).html(message);
                    _divPortalMessageInfo.show();
                    break;
            }
        }

        if (SadaCore.MessageBoxControl.ScrollToTopOnShow) {

            $(window).scrollTop(0);
        }
    };

    _Init();
};

SadaCore.MessageBoxControl.ScrollToTopOnShow = true;
SadaCore.MessageBoxControl.ShowLoaderOnRedirect = false;

SadaCore.QueryString = new function () {

    var _items = new Array();
    var that = this;

    this.Parse = function (query, ignoreCase) {

        if (query != null) {

            var ignoreCase = typeof ignoreCase != 'undefined' ? ignoreCase : true;

            var qs = (query.indexOf("?") != -1 ? query.substr(query.indexOf("?") + 1) : query).split('&');

            var items = new Object();
            if (qs != null && qs.length > 0) {

                for (var i = 0; i < qs.length; ++i) {
                    var item = qs[i].split('=');
                    if (item != null && item != '') {

                        var key = ignoreCase ? item[0].toLowerCase() : item[0];
                        var value = decodeURIComponent(item[1]);

                        items[key] = value;
                    }
                }
            }
            return items;
        }

        return new Array();
    };
    GetItems = function () {

        if (that._items == null || that._items.length == 0) {
            that._items = that.Parse(window.location.search);
        }
        return that._items;
    };
    this.Items = that.Parse(window.location.search)
};

SadaCore.HttpContext =
{
    Request: new function (target) {
        var _that = this;
        this.Target = target ? target : document;
        this.Href = new String(this.Target.location.href);

        this.QueryString = function (key, ignoreCase) {
            var ignoreCase = typeof ignoreCase != 'undefined' ? ignoreCase : true;

            var value = SadaCore.QueryString.Parse(_that.Target.location.search, ignoreCase)[key];

            if (!value) {
                return null;
            }

            return value;
        }
    },
    Response:
	{
	    Redirect: function (redirectUrl) {

	        SadaCore.HttpContext.Redirecting = true;
	        window.location.href = redirectUrl;
	    },
	    Refresh: function () {

	        SadaCore.HttpContext.Response.Redirect(window.location.href);
	    }
	},
    Redirecting: false
};

SadaCore.Web =
{
    Cookies:
    {
        Set: function (name, value, days) {

            var expires = new String();

            if (days) {

                var date = new Date();
                date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
                expires = ";expires=" + date.toGMTString();
            }
            else {

                expires = "";
            }
            document.cookie = name + "=" + value + expires;
        },
        Get: function (name, defaultValue) {
            var nameEQ = name + "=";
            var cookies = document.cookie.split(';');

            for (var i = 0; i < cookies.length; i++) {

                var cookie = cookies[i];
                while (cookie.charAt(0) == ' ') {
                    cookie = cookie.substring(1, cookie.length);
                }
                if (cookie.indexOf(nameEQ) == 0) {

                    return cookie.substring(nameEQ.length, cookie.length);
                }
            }
            return defaultValue;
        },
        Remove: function (name) {

            document.cookie = name + "=" + escape('') + "; expires=Fri, 31 Dec 1999 23:59:59 GMT;";
        }
    }
}

SadaCore.QueryStringManager = function (query) {

    var _that = this;
    var _coll = new Array();

    QueryStringManager = function (query) {
        if (typeof query == 'undefined') {

            _coll = SadaCore.QueryString.Items;
        }
        else if (query != null) {

            _coll = SadaCore.QueryString.Parse(query);
        }
    };

    this.GetItem = function (name) {
        return this.ContainsKey(name.toLowerCase()) ? _coll[name.toLowerCase()] : null;
    };

    this.SetItem = function (name, value) {
        var isItemSetted = false;
        if (name != null && name != "") {
            _coll[name.toLowerCase()] = value;

            isItemSetted = true;
        }

        return isItemSetted;
    };

    this.RemoveItem = function (name) {

        var removed = false;
        var coll = new Array();
        for (var key in _coll) {

            if (key.toLowerCase() != name.toLowerCase())
                coll[key.toLowerCase()] = _coll[key.toLowerCase()];
            else
                removed = true;
        }
        _coll = coll;

        return removed;
    };

    this.ContainsKeyWithValue = function (name) {
        if (name != null && name != "") {
            for (var key in _coll) {
                if (key != null && key != "" && name.toLowerCase() == key.toLowerCase()) {
                    return !_coll[name.toLowerCase()] != null && _coll[name.toLowerCase()] != "";
                }
            }
        }
        return false;
    };

    this.ContainsKey = function (name) {
        return _coll[name.toLowerCase()] != undefined;
    };

    this.ToString = function (prefixWithQuestionMark) {

        //default false
        prefixWithQuestionMark = (typeof prefixWithQuestionMark == 'undefined') ? false : prefixWithQuestionMark;

        var queryStrings = "";
        if (_coll != null && _coll[""] != "undefined") {
            var pairs = new Array();
            for (var key in _coll) {

                pairs.push(key + "=" + encodeURIComponent(_coll[key]));
            }
            queryStrings = prefixWithQuestionMark ? "?" : "";
            queryStrings += pairs.join("&");
        }

        if (queryStrings == "?") {

            queryStrings = "";
        }
        return queryStrings;
    };

    QueryStringManager(query);
};

SadaCore.Hash = function () {
    this.length = 0;
    this.items = new Array();
    for (var i = 0; i < arguments.length; i += 2) {
        if (typeof (arguments[i + 1]) != 'undefined') {
            this.items[arguments[i]] = arguments[i + 1];
            this.length++;
        }
    }

    this.removeItem = function (in_key) {
        var tmp_previous;
        if (typeof (this.items[in_key]) != 'undefined') {
            this.length--;
            var tmp_previous = this.items[in_key];
            delete this.items[in_key];
        }

        return tmp_previous;
    }

    this.getItem = function (in_key) {
        return this.items[in_key];
    }

    this.setItem = function (in_key, in_value) {
        var tmp_previous;
        if (typeof (in_value) != 'undefined') {
            if (typeof (this.items[in_key]) == 'undefined') {
                this.length++;
            }
            else {
                tmp_previous = this.items[in_key];
            }

            this.items[in_key] = in_value;
        }

        return tmp_previous;
    }

    this.hasItem = function (in_key) {
        return typeof (this.items[in_key]) != 'undefined';
    }

    this.clear = function () {
        for (var i in this.items) {
            delete this.items[i];
        }

        this.length = 0;
    }
};

String.prototype.beginsWith = function (str) {
    return (this.match("^" + str) == str);
};
String.prototype.endsWith = function (str) {
    return (this.match(str + '$') == str);
};

String.prototype.trim = function (trimChars) {

    trimChars = typeof trimChars != "undefined" ? trimChars : "";

    return this.replace(/^\s+|\s+$/g, trimChars);
};

String.prototype.trimLeft = function (trimChars) {

    trimChars = typeof trimChars != "undefined" ? trimChars : "";

    return this.replace(/^\s+/, trimChars);
};

String.prototype.trimRight = function (trimChars) {

    trimChars = typeof trimChars != "undefined" ? trimChars : "";

    return this.replace(/\s+$/, trimChars);
};

String.prototype.padLeft = function (padString, length) {
    var str = this;
    while (str.length < length)
        str = padString + str;
    return str;
};

String.prototype.padRight = function (padString, length) {
    var str = this;
    while (str.length < length)
        str = str + padString;
    return str;
};

String.IsNullOrEmpty = function (value) {

    return ((typeof value == "undefined") || (value == null) || (value.length == 0));
};
String.IsNullOrWhiteSpace = function (value) {

    return ((typeof value == "undefined") || (value == null) || (value.length == 0) || (value.toString().trim().length == 0));
};

String.Format = function (value) {

    var i = arguments.length;

    while (i-- > 0) {

        value = value.replace(new RegExp('\\{' + (i - 1) + '\\}', 'gm'), arguments[i]);
    }

    return value;
};

String.Join = function (separator, value, startIndex, count) {

    if (!separator) separator = "";
    if (!startIndex) startIndex = 0;
    if (!count) count = value.length;
    if (count == 0) return "";
    var length = 0;
    var end = (startIndex + count) - 1;
    var s = new String();
    for (var i = startIndex; i <= end; i++) {
        if (i > startIndex) s += separator;
        s += value[i];
    }
    return s;
};

function StringPadLeft(text, size, char) {

    var t = text.toString();

    var paddedString = '';
    try {

        while ((paddedString.length + t.length) < size) {

            paddedString += char;
        }

        paddedString += t;
    }
    catch (e) {

        paddedString = text;
    }
    return paddedString;
}

SadaCore.Text =
{
    StringBuilder: function (value) {

        var _parts = new Array();

        this.Append = function (value) {
            var results = true;

            if (typeof (value) == 'undefined') {
                results = false;
            } else {
                _parts.push(value);
            }
            return results;
        };
        this.AppendLine = function (value) {
            return this.Append(value + '\r\n');
        };
        this.AppendFormat = function (value) {

            this.Append(String.Format.apply(value, arguments));
        };
        this.Clear = function () {
            if (_parts.length > 0) {
                _parts.splice(0, _parts.length);
            }
        };
        this.IsEmpty = function () {
            return (_parts.length == 0);
        };
        this.ToString = function (delimiter) {
            return _parts.join(delimiter || '');
        };
        this.ToArray = function (delimiter) {
            return _parts;
        };
        this.Init = function () {
            if (value) this.Append(value);
        };
        this.Init();
    },
    ToCamelCase: function (value) {

        var regExp = new RegExp("([A-Z])([A-Z]+)", "ig");

        function ConvertCase(a, b, c) {

            return b.toUpperCase() + c.toLowerCase();
        };

        var result = value.replace(regExp, ConvertCase);

        return result;
    },
    HtmlSymbolCodes: {

        0x0022: "quot",
        0x0026: "amp",
        0x003c: "lt",
        0x003e: "gt",
        0x00a0: "nbsp",
        0x00a1: "iexcl",
        0x00a2: "cent",
        0x00a3: "pound",
        0x00a4: "curren",
        0x00a5: "yen",
        0x00a6: "brvbar",
        0x00a7: "sect",
        0x00a8: "uml",
        0x00a9: "copy",
        0x00aa: "ordf",
        0x00ab: "laquo",
        0x00ac: "not",
        0x00ad: "shy",
        0x00ae: "reg",
        0x00af: "macr",
        0x00b0: "deg",
        0x00b1: "plusmn",
        0x00b2: "sup2",
        0x00b3: "sup3",
        0x00b4: "acute",
        0x00b5: "micro",
        0x00b6: "para",
        0x00b7: "middot",
        0x00b8: "cedil",
        0x00b9: "sup1",
        0x00ba: "ordm",
        0x00bb: "raquo",
        0x00bc: "frac14",
        0x00bd: "frac12",
        0x00be: "frac34",
        0x00bf: "iquest",
        0x00c0: "Agrave",
        0x00c1: "Aacute",
        0x00c2: "Acirc",
        0x00c3: "Atilde",
        0x00c4: "Auml",
        0x00c5: "Aring",
        0x00c6: "AElig",
        0x00c7: "Ccedil",
        0x00c8: "Egrave",
        0x00c9: "Eacute",
        0x00ca: "Ecirc",
        0x00cb: "Euml",
        0x00cc: "Igrave",
        0x00cd: "Iacute",
        0x00ce: "Icirc",
        0x00cf: "Iuml",
        0x00d0: "ETH",
        0x00d1: "Ntilde",
        0x00d2: "Ograve",
        0x00d3: "Oacute",
        0x00d4: "Ocirc",
        0x00d5: "Otilde",
        0x00d6: "Ouml",
        0x00d7: "times",
        0x00d8: "Oslash",
        0x00d9: "Ugrave",
        0x00da: "Uacute",
        0x00db: "Ucirc",
        0x00dc: "Uuml",
        0x00dd: "Yacute",
        0x00de: "THORN",
        0x00df: "szlig",
        0x00e0: "agrave",
        0x00e1: "aacute",
        0x00e2: "acirc",
        0x00e3: "atilde",
        0x00e4: "auml",
        0x00e5: "aring",
        0x00e6: "aelig",
        0x00e7: "ccedil",
        0x00e8: "egrave",
        0x00e9: "eacute",
        0x00ea: "ecirc",
        0x00eb: "euml",
        0x00ec: "igrave",
        0x00ed: "iacute",
        0x00ee: "icirc",
        0x00ef: "iuml",
        0x00f0: "eth",
        0x00f1: "ntilde",
        0x00f2: "ograve",
        0x00f3: "oacute",
        0x00f4: "ocirc",
        0x00f5: "otilde",
        0x00f6: "ouml",
        0x00f7: "divide",
        0x00f8: "oslash",
        0x00f9: "ugrave",
        0x00fa: "uacute",
        0x00fb: "ucirc",
        0x00fc: "uuml",
        0x00fd: "yacute",
        0x00fe: "thorn",
        0x00ff: "yuml",
        0x0152: "OElig",
        0x0153: "oelig",
        0x0160: "Scaron",
        0x0161: "scaron",
        0x0178: "Yuml",
        0x0192: "fnof",
        0x02c6: "circ",
        0x02dc: "tilde",
        0x0391: "Alpha",
        0x0392: "Beta",
        0x0393: "Gamma",
        0x0394: "Delta",
        0x0395: "Epsilon",
        0x0396: "Zeta",
        0x0397: "Eta",
        0x0398: "Theta",
        0x0399: "Iota",
        0x039a: "Kappa",
        0x039b: "Lambda",
        0x039c: "Mu",
        0x039d: "Nu",
        0x039e: "Xi",
        0x039f: "Omicron",
        0x03a0: "Pi",
        0x03a1: "Rho",
        0x03a3: "Sigma",
        0x03a4: "Tau",
        0x03a5: "Upsilon",
        0x03a6: "Phi",
        0x03a7: "Chi",
        0x03a8: "Psi",
        0x03a9: "Omega",
        0x03b1: "alpha",
        0x03b2: "beta",
        0x03b3: "gamma",
        0x03b4: "delta",
        0x03b5: "epsilon",
        0x03b6: "zeta",
        0x03b7: "eta",
        0x03b8: "theta",
        0x03b9: "iota",
        0x03ba: "kappa",
        0x03bb: "lambda",
        0x03bc: "mu",
        0x03bd: "nu",
        0x03be: "xi",
        0x03bf: "omicron",
        0x03c0: "pi",
        0x03c1: "rho",
        0x03c2: "sigmaf",
        0x03c3: "sigma",
        0x03c4: "tau",
        0x03c5: "upsilon",
        0x03c6: "phi",
        0x03c7: "chi",
        0x03c8: "psi",
        0x03c9: "omega",
        0x03d1: "thetasym",
        0x03d2: "upsih",
        0x03d6: "piv",
        0x2002: "ensp",
        0x2003: "emsp",
        0x2009: "thinsp",
        0x200c: "zwnj",
        0x200d: "zwj",
        0x200e: "lrm",
        0x200f: "rlm",
        0x2013: "ndash",
        0x2014: "mdash",
        0x2018: "lsquo",
        0x2019: "rsquo",
        0x201a: "sbquo",
        0x201c: "ldquo",
        0x201d: "rdquo",
        0x201e: "bdquo",
        0x2020: "dagger",
        0x2021: "Dagger",
        0x2022: "bull",
        0x2026: "hellip",
        0x2030: "permil",
        0x2032: "prime",
        0x2033: "Prime",
        0x2039: "lsaquo",
        0x203a: "rsaquo",
        0x203e: "oline",
        0x2044: "frasl",
        0x20ac: "euro",
        0x2111: "image",
        0x2118: "weierp",
        0x211c: "real",
        0x2122: "trade",
        0x2135: "alefsym",
        0x2190: "larr",
        0x2191: "uarr",
        0x2192: "rarr",
        0x2193: "darr",
        0x2194: "harr",
        0x21b5: "crarr",
        0x21d0: "lArr",
        0x21d1: "uArr",
        0x21d2: "rArr",
        0x21d3: "dArr",
        0x21d4: "hArr",
        0x2200: "forall",
        0x2202: "part",
        0x2203: "exist",
        0x2205: "empty",
        0x2207: "nabla",
        0x2208: "isin",
        0x2209: "notin",
        0x220b: "ni",
        0x220f: "prod",
        0x2211: "sum",
        0x2212: "minus",
        0x2217: "lowast",
        0x221a: "radic",
        0x221d: "prop",
        0x221e: "infin",
        0x2220: "ang",
        0x2227: "and",
        0x2228: "or",
        0x2229: "cap",
        0x222a: "cup",
        0x222b: "int",
        0x2234: "there4",
        0x223c: "sim",
        0x2245: "cong",
        0x2248: "asymp",
        0x2260: "ne",
        0x2261: "equiv",
        0x2264: "le",
        0x2265: "ge",
        0x2282: "sub",
        0x2283: "sup",
        0x2284: "nsub",
        0x2286: "sube",
        0x2287: "supe",
        0x2295: "oplus",
        0x2297: "otimes",
        0x22a5: "perp",
        0x22c5: "sdot",
        0x2308: "lceil",
        0x2309: "rceil",
        0x230a: "lfloor",
        0x230b: "rfloor",
        0x2329: "lang",
        0x232a: "rang",
        0x25ca: "loz",
        0x2660: "spades",
        0x2663: "clubs",
        0x2665: "hearts",
        0x2666: "diams"
    },
    HtmlChars: {}
};

for (var property in SadaCore.Text.HtmlSymbolCodes) {

    var name = SadaCore.Text.HtmlSymbolCodes[property];
    SadaCore.Text.HtmlChars[name] = String.fromCharCode(property);
}

SadaCore.DisableTextSelection = function (target) {

    if (typeof target.onselectstart != "undefined") {

        target.onselectstart = function () { return false }
    }
    else if (typeof target.style.MozUserSelect != "undefined") {

        target.style.MozUserSelect = "none"
    }
    else {

        target.onmousedown = function () { return false }
    }
    target.style.cursor = "default"
};

SadaCore.Loader = {

    Enabled: true,
    Text: '<img src="/WebResources/Images/ajax-loader.gif" title="..." />',
    StartHandler: null,
    StopHandler: null,
    CssClass: 'loader',
    AddInlineCss: true,
    Html: null,
    Instance: null,
    IgnoreRedirectState: false,
    BeforeShow: null,
    Show: function () {

        if (SadaCore.Loader.StartHandler != null) {

            SadaCore.Loader.StartHandler();
        }
        else {

            if (SadaCore.Loader.Enabled) {

                if (SadaCore.Loader.BeforeShow != null) {

                    SadaCore.Loader.BeforeShow();
                }

                var ajaxLoader = $("." + SadaCore.Loader.CssClass);
                if (!ajaxLoader.length) {

                    if (SadaCore.Loader.Html) {

                        ajaxLoader = $(SadaCore.Loader.Html);
                    }
                    else {

                        ajaxLoader = $(String.Format('<div class="{0}">{1}</div>', SadaCore.Loader.CssClass, SadaCore.Loader.Text));

                        if (SadaCore.Loader.AddInlineCss) {
                            ajaxLoader.css({
                                position: 'fixed',
                                width: '20px',
                                height: '20px',
                                zIndex: '1',
                                left: '5px',
                                bottom: '5px'
                            });
                        }
                    }
                    ajaxLoader.appendTo(SadaCore.Elements.Body);
                }

                SadaCore.Loader.Instance = ajaxLoader;

                ajaxLoader.show();
            }
        }
    },
    Hide: function () {

        if (!SadaCore.Loader.IgnoreRedirectState && SadaCore.HttpContext.Redirecting === false) {

            if (typeof (SnTCms) != 'undefined' && typeof (SnTCms.SessionLease) != 'undefined' && typeof (SnTCms.SessionLease.Reset) != 'undefined') {

                //session refreshed
                SnTCms.SessionLease.Reset();
            }

            if (SadaCore.Loader.StopHandler != null) {

                SadaCore.Loader.StopHandler();
            }
            else {

                var loader = SadaCore.Loader.Instance;

                if (loader) {

                    loader.hide();
                }
            }
        }
    }
};

SadaCore.GetDataFromJson = function (jsonData) {

    try {

        if (jsonData.hasOwnProperty('d')) {
            return jsonData.d;
        } else {
            return jsonData;
        }
    }
    catch (e) {

        var message = "The data is not retrieved, check the URL for the web service. \n" + e.message;

        SadaCore.HandleException(message, "SadaCore.GetDataFromJson");
    }
    return null;
};

SadaCore.Ajax = function (url, paramsdata, onSucessFn, onErrorFn, optionCallBack, optionCallbackData, options) {

    var jsonData = JSON.stringify(paramsdata);

    var success = function (jsonData, stat) {

        if (stat == "success") {

            try {
                var parsedJsonData = SadaCore.GetDataFromJson(jsonData);

                onSucessFn(parsedJsonData, optionCallBack, optionCallbackData);
            }
            catch (e) {

                SadaCore.HandleException(e, "SadaCore.Ajax.SuccessCallback");

                if (typeof onErrorFn != 'undefined') {

                    onErrorFn(e);
                }
            }
        }
    };
    var error = function (data, textStatus) {

        SadaCore.HandleAspNetException(data, "Ajax.error");

        if (typeof onErrorFn != 'undefined') {

            onErrorFn(data.responseText);
        }
    };

    var config = $.extend({}, {
        type: "POST",
        url: url,
        contentType: "application/json; charset=utf-8",
        data: jsonData,
        dataType: "json",
        success: success,
        error: error
    }, options);

    var xhr = $.ajax(config);

    return xhr;
};

SadaCore.Ajax.Initialize = function () {

    SadaCore.Elements.Body.ajaxStart(SadaCore.Loader.Show);
    SadaCore.Elements.Body.ajaxStop(SadaCore.Loader.Hide);
};

SadaCore.Ajax.ShowAlertOnError = false;

SadaCore.Ajax.OnError = function (message) {

    SadaCore.Audit.push(
	{
	    Time: SadaCore.DateTime.Now().ToString("dd.MM.yyyy. hh:ss.fff"),
	    Message: message
	});

    if (typeof (console) != 'undefined' && typeof (console.log) != 'undefined') {

        console.log(message);
    }
    else if (SadaCore.Ajax.ShowAlertOnError) {

        alert(message);
    }
};

SadaCore.ServiceProxy = function (url, options) {

    var _me = this;
    this.Url = url;
    this.Xhr = null;
    this.IsAsync = true;
    this.Config = null;
    this.DefaultErrorMessage = "Ajax poziv:\nDogodila se greška.";

    this.Abort = function () {

        if (_me.Xhr != null && _me.Xhr.abort) {

            _me.Xhr.abort();
        }
    };
    this.OnError = function (message) {

        if (!String.IsNullOrEmpty(message)) {

            message = String.Format("{0}\n'{1}'", _me.DefaultErrorMessage, message);
        }
        else {
            message = _me.DefaultErrorMessage;
        }

        SadaCore.Ajax.OnError(message);
    };

    this.Invoke = function (methodName, params, successFn, errorFn) {

        var url = !String.IsNullOrEmpty(_me.Url) ? String.Format("{0}/{1}", _me.Url, methodName) : methodName;
        errorFn = typeof errorFn != 'undefined' ? errorFn : _me.OnError;

        _me.Xhr = SadaCore.Ajax(url, params, successFn, errorFn, null, null, _me.Config);
    };

    function Init() {

        _me.Config = $.extend({}, {
            async: _me.IsAsync
        }, options);
    };

    Init();
};

SadaCore.ConsoleClear = function () {

    $("div#debugConsole table.consoleData tr:not('table.consoleData tr:last')").remove();
};

SadaCore.ConsolePrint = function (text) {

    var time = new Date();

    text = StringPadLeft(time.getHours(), 2, '0') + ':' + StringPadLeft(time.getMinutes(), 2, '0') + ':' + StringPadLeft(time.getSeconds(), 2, '0') + ':' + StringPadLeft(time.getMilliseconds(), 3, '0') + '  ' + text;

    SadaCore.Audit.push(text);

    if (SadaCore.Debuging) {

        if ($.browser.mozilla) {

            console.log(text);

            $("div#debugConsole").hide();
        }
        else {

            var debugConsole = $("div#debugConsole");

            if (!debugConsole.length) {

                debugConsole = $('<div id="debugConsole" Style="z-index:5000;vertical-align:top;overflow:auto;display:none;position:absolute;width:100%;height:100px;bottom:10px;color:Black;margin-left:5px !important;margin-right:5px !important;border: 1px dotted #BBBBBB;background-color:White;"><span><a href="#" style="color:black; float:right; width:30px;" onclick="SadaCore.ConsoleClear(); return false">Clear</a></span><table width="100%" class="consoleData"><tr><td>&nbsp;&nbsp;</td></tr></table></div>');

                $("body").append(debugConsole);

                debugConsole.width($(window).width() - 20);

                debugConsole.show();
            }
            var consoleData = debugConsole.find("table.consoleData");

            text = text.replace('\n', '<br/>')

            var tr = $('<tr><td style="color:black;">' + text + '</td></tr>');

            consoleData.prepend(tr);
        }
    }
};

SadaCore.HandleException = function (e, methodName) {

    var message = methodName != undefined ? "Method: " + methodName + "\n" : '';

    if (typeof e == 'object') {
        message += ("File: " + e.fileName + "\n");
        message += ("Line Number: " + e.lineNumber + "\n");
        message += "Type: " + e.name + "\n";
        message += "Message: " + e.message + "\n";
        message += "Stack Trace: " + e.stack + "\n";
    }
    else {
        message += e;
    }

    SadaCore.ConsolePrint(message);
};

SadaCore.HandleAspNetException = function (e, methodName) {

    var ex = $.parseJSON(e.responseText);

    if (ex == null) {

        SadaCore.HandleException(e, methodName);
    }
    else {

        var message = methodName != undefined ? "Method: " + methodName + "\n" : '';

        if (SadaCore.Helper.IsObject(ex)) {
            message += "Type: " + ex.ExceptionType + "\n";
            message += "Message: " + ex.Message + "\n";
            message += "Stack Trace: " + ex.StackTrace + "\n";
        }

        SadaCore.ConsolePrint(message);
    }
};

SadaCore.Debug = function (message) {

    SadaCore.ConsolePrint(message);
};

jQuery.expr[':'].data = function (elem, index, m) {

    m[0] = m[0].replace(/:data\(|\)$/g, '');

    var regex = new RegExp('([\'"]?)((?:\\\\\\1|.)+?)\\1(,|$)', 'g'),
        key = regex.exec(m[0])[2],
        val = regex.exec(m[0])[2];

    return val ? jQuery(elem).data(key) == val : !!jQuery(elem).data(key);
};

jQuery.expr[":"].asp = function (a, i, m) {

    var id = $(a).attr('id');

    return id && id.endsWith("_" + m[3]);
};

SadaCore.Parser =
{
    Parse: function (object, value) {

        var result = null;
        switch (typeof (object)) {
            case "number":
                result = SadaCore.Parser.TryParseFloat(value);
                break;
            case "string":
                result = value;
                break;
            case "boolean":
                result = SadaCore.Parser.TryParseBoolean(value);
                break;
            case "object":
                result = value;
                if (typeof (object["getDate"]) == "function") {

                    result = new Date().GetFromString(value);
                }
                break;
            default:
                result = value;
                break;
        }
        return result;
    },
    TryParseInt: function (strValue, defaultValue) {

        var defaultVal = typeof defaultValue != 'undefined' ? SadaCore.Parser.TryParseInt(defaultValue) : 0;

        var retValue = SadaCore.Parser.TryParseIntNullable(strValue);

        if (retValue == null) {

            retValue = defaultVal;
        }

        return retValue;
    },
    TryParseIntNullable: function (strValue) {

        var retValue = null;

        if (typeof strValue != "undefined" && strValue != null && !String.IsNullOrWhiteSpace(strValue) && !isNaN(strValue)) {

            retValue = parseInt(strValue);
        }

        return retValue;
    },
    TryParseFloat: function (strValue, defaultValue) {

        var defaultVal = typeof defaultValue != 'undefined' ? SadaCore.Parser.TryParseFloatNullable(defaultValue) : 0.0;

        var retValue = SadaCore.Parser.TryParseFloatNullable(strValue);

        if (retValue == null) {

            retValue = defaultVal;
        }

        return retValue;
    },
    TryParseFloatNullable: function (strValue) {

        var retValue = null;

        if (typeof strValue != "undefined" && strValue != null && !String.IsNullOrWhiteSpace(strValue) && !isNaN(strValue)) {

            retValue = parseFloat(strValue);
        }

        return retValue;
    },
    TryParseBoolean: function (value) {

        var results = new String(value).toLowerCase();
        if (results == "true" || results == "1" || results == "-1" || results == "on" || results == "yes") {

            return true;
        }

        return false;
    },
    TryParseDateTimeNullable: function (o) {

        var retValue = null;

        if (typeof o != "undefined" && o != null) {

            retValue = SadaCore.Parser.Parse(new Date(), o);

            if (retValue != null && retValue.getYear() == -1901) {

                retValue = null;
            }
        }

        return retValue;
    }
};

SadaCore.Parser.TryParseDouble = SadaCore.Parser.TryParseFloat;
SadaCore.Parser.TryParseDoubleNullable = SadaCore.Parser.TryParseFloatNullable;

SadaCore.ExecutionQueue = function (autoStart) {

    var _that = this;
    var _items = [];
    var _counter = 0;
    var _executing = typeof autoStart == 'undefined' ? false : !autoStart;

    this.Enqueue = function (callback) {

        //SadaCore.Debug("SadaCore.ExecutionQueue.Enqueue");

        _items[_items.length] = callback;
        if (_executing == false) {

            _that.Dequeue();
        }
    };
    this.Dequeue = function () {

        //SadaCore.Debug("SadaCore.ExecutionQueue.Dequeue");

        if (_items[_counter]) {

            _executing = true;
            _items[_counter]();
            delete _items[_counter];
            _counter++;

            setTimeout(_that.Dequeue, 1)
        }
        else {

            _executing = false
        }
    };
    this.ForceDequeue = function () {

        _executing = false;

        _that.Dequeue();
    };
    this.Flush = function () {

        _items = [];
        _counter = 0;
        _executing = false;
    }
};

SadaCore.HttpUtility =
{
    HtmlDecode: function (s) {
        var out = "";
        if (s != null) {
            var l = s.length;
            for (var i = 0; i < l; i++) {
                var ch = s.charAt(i);
                if (ch == '&') {
                    var semicolonIndex = s.indexOf(';', i + 1);
                    if (semicolonIndex > 0) {
                        var entity = s.substring(i + 1, semicolonIndex);
                        if (entity.length > 1 && entity.charAt(0) == '#') {
                            if (entity.charAt(1) == 'x' || entity.charAt(1) == 'X') {
                                ch = String.fromCharCode(eval('0' + entity.substring(1)));
                            } else {
                                ch = String.fromCharCode(eval(entity.substring(1)));
                            }
                        } else {
                            ch = SadaCore.Text.HtmlChars[entity] ? SadaCore.Text.HtmlChars[entity] : '';
                        }
                        i = semicolonIndex;
                    }
                }
                out += ch;
            }
        }
        return out;
    },
    HtmlEncode: function (html) {

        var div = document.createElement("div");
        var text = document.createTextNode(html);
        div.appendChild(text);
        html = div.innerHTML;
        delete div;

        return html;
    },
    UrlEncode: function (url) {

        url = (url + '').toString();

        url = encodeURIComponent(url).replace(/!/g, '%21')
                                     .replace(/'/g, '%27')
                                     .replace(/\(/g, '%28')
                                     .replace(/\)/g, '%29')
                                     .replace(/\*/g, '%2A')
                                     .replace(/%20/g, '+');
        return url;
    },
    UrlDecode: function (url) {

        url = decodeURIComponent(url.replace(/\+/g, ' '));

        return url;
    },
    ParseQueryString: SadaCore.QueryString.Parse
};

SadaCore.Random = function () {

    this.Next = function (minValue, maxValue) {
        switch (arguments.length) {
            case 0:
                maxValue = Math.pow(2, 31);
                minValue = 0;
                break;
            case 1:
                maxValue = arguments[0];
                minValue = 0;
                break;
            case 2:
                break;
            default:
                return 0;
                break;
        }
        var number = minValue;
        if (maxValue > minValue) {
            number = Math.floor(Math.random() * (maxValue - minValue)) + minValue;
        }
        return number;
    };
    this.NextBytes = function (buffer) {
        var length = buffer.length;
        for (var i = 0; i < length; i++) {
            buffer[i] = this.Next(0, 256);
        }
        return buffer;
    };
};

SadaCore.Guid = function (guid) {
    this.Type = "Guid";
    this.Bytes = new Array;
    this.ByteOrder = [3, 2, 1, 0, 5, 4, 7, 6, 8, 9, 10, 11, 12, 13, 14, 15];

    this.ToString = function (format) {

        format = format ? format : "D";
        var addHyphens = ("DBP".indexOf(format) > -1);
        var guid = new String;
        for (var i = 0; i < 16; i++) {
            if (addHyphens) guid += (i == 4 || i == 6 || i == 8 || i == 10 ? "-" : "");
            var pos = this.ByteOrder[i];
            guid += this.numberToHex(this.Bytes[pos]);
        }
        if (format == "B") guid = "{" + guid + "}";
        if (format == "P") guid = "(" + guid + ")";
        return guid;
    };
    this.toString = this.ToString;

    this.ToByteArray = function () {
        return this.Bytes;
    };
    this.Equals = function (value) {
        var guid = value;
        var results = true;
        if (typeof (value) != "object") {
            guid = new SadaCore.Guid(value);
        }
        for (var i = 0; i < 16; i++) {
            if (this.Bytes[i] != guid.Bytes[i]) {
                results = false;
                break;
            }
        }
        return results;
    };
    this.numberToHex = function (value) {
        var hex = ((value <= 0xF) ? "0" : "");
        hex += value.toString(16);
        return hex;
    };
    this.GuidStringToBytes = function (value) {

        var regExp = new RegExp("[{}\(\)-]", "g");
        var guid = value.replace(regExp, "");

        var bytes = new Array();
        for (var i = 0; i < 16; i++) {
            var pos = this.ByteOrder[i];
            var b1 = guid.charAt(pos * 2);
            var b2 = guid.charAt(pos * 2 + 1);
            bytes.push(unescape("%" + b1 + b2).charCodeAt(0));
        }
        return bytes;
    };
    this.Init = function () {
        this.Bytes = new Array();

        var a0 = arguments[0];
        switch (typeof (a0)) {
            case "null":
                for (var i = 0; i < 16; i++) this.Bytes.push(0);
                break;
            case "undefined":
                for (var i = 0; i < 16; i++) this.Bytes.push(0);
                break;
            case "string":
                this.Bytes = this.GuidStringToBytes(a0);
                break;
            case "object":
                if (a0.Type && a0.Type == "Guid") {

                    for (var i = 0; i < 16; i++) {
                        this.Bytes.push(a0.Bytes[i]);
                    }
                }
                else {
                    for (var i = 0; i < 16; i++) {
                        this.Bytes.push(a0[i]);
                    }
                }
                break;
            default:
                break;
        }
    }
    this.Init.apply(this, arguments);
};

SadaCore.Guid.Empty = new SadaCore.Guid("00000000-0000-0000-0000-000000000000");

SadaCore.Guid.NewGuid = function () {

    var bytes = new Array();
    for (var i = 0; i < 16; i++) {

        var dec = Math.floor(Math.random() * 0xFF);
        bytes.push(dec);
    }
    var guid = new SadaCore.Guid(bytes);
    return guid;
};

Math.ShiftRight = function (number, positions) {

    var h = Math.pow(2, positions);
    var d = number & (h - 1);
    var n = number - d;
    return n / h;
};

Math.ShiftLeft = function (number, positions) {
    return number * Math.pow(2, positions);
};

SadaCore.DateTime = {};

SadaCore.DateTime.Now = function () {

    return new Date();
}

SadaCore.DateTime.Globalization =
{
    HR:
    {
        Separator: ".",
        YearMin: 1900,
        YearMax: 2100,
        DateFormat: "dd.MM.yyyy",
        XFormat: "dd.MM.yyyy HH:mm:ss.fffzzz",
        OutlookFormat: "dd.MM.yyyy HH:mm",
        www: ["Ned", "Pon", "Uto", "Sri", "Čet", "Pet", "Sub"],
        ddd: ["Ned", "Pon", "Uto", "Sri", "Čet", "Pet", "Sub"],
        dddd: ["Nedjelja", "Ponedjeljak", "Utorak", "Srijeda", "Četvrtak", "Petak", "Subota"],
        MMM: ["Sij", "Velj", "Ožu", "Tra", "Svi", "Lip", "Srp", "Kol", "Ruj", "Lis", "Stu", "Pro"],
        MMMM: ["Siječanj", "Veljača", "Ožujak", "Travanj", "Svibanj", "Lipanj", "Srpanj", "Kolovoz", "Rujan", "Listopad", "Studeni", "Prosinac"],
        Expression: new RegExp("([0-9][0-9]).(0[1-9]|1[012]).(0[1-9]|[12][0-9]|3[01])"),
        ExpressionUtcDate: new RegExp("(0[1-9]|[12][0-9]|3[01]).(0[1-9]|1[012]).([0-9][0-9][0-9][0-9])"),
        ExpressionUtcDatePositions: { Year: '$3', Month: '$2', Day: '$1' },
        ExpressionUtcTime: new RegExp("([01][0-9]|[2][0123]):([012345][0-9]):([012345][0-9])"),
        ExpressionUtcMs: new RegExp("\.([0-9]+)"),
        ExpressionZone: new RegExp("([+-])([01][0-9]|[2][0123]):([012345][0-9])"),
        ExpressionUtc: new RegExp(new RegExp("(0[1-9]|[12][0-9]|3[01]).(0[1-9]|1[012]).([0-9][0-9][0-9][0-9])").toString() + "[T ]" + new RegExp("([01][0-9]|[2][0123]):([012345][0-9]):([012345][0-9])").toString()),
        Expressions:
        {
            Default: new RegExp("(0[1-9]|1[012])/(0[1-9]|[12][0-9]|3[01])/([0-9][0-9])"),
            UtcDate: new RegExp("([0-9][0-9][0-9][0-9])-(0[1-9]|1[012])-(0[1-9]|[12][0-9]|3[01])"),
            UtcTime: new RegExp("([01][0-9]|[2][0123]):([012345][0-9]):([012345][0-9])"),
            UtcMs: new RegExp("\.([0-9]+)"),
            Zone: new RegExp("([+-])([01][0-9]|[2][0123]):([012345][0-9])"),
            Utc: new RegExp("([0-9][0-9][0-9][0-9])-(0[1-9]|1[012])-(0[1-9]|[12][0-9]|3[01])" + "[T ]" + "([01][0-9]|[2][0123]):([012345][0-9]):([012345][0-9])")
        }
    },
    EN:
    {
        Separator: "/",
        YearMin: 1900,
        YearMax: 2100,
        DateFormat: "dd/mm/yyyy",
        XFormat: "yyyy-MM-ddTHH:mm:ss.fffzzz",
        OutlookFormat: "yyyy-MM-dd HH:mm",
        www: ["Sun", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat"],
        ddd: ["Sun", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat"],
        dddd: ["Sunday", "Monday", "Tuesday", "Wednesday", "Thuesday", "Friday", "Saturday"],
        MMM: ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"],
        MMMM: ["January", "February", "March", "April", "May", "June", "July", "Augst", "Sepember", "October", "Novmber", "December"],
        Expression: new RegExp("(0[1-9]|[12][0-9]|3[01]).(0[1-9]|1[012]).([0-9][0-9])"),
        ExpressionUtcDate: new RegExp("([0-9][0-9][0-9][0-9])-(0[1-9]|1[012])-(0[1-9]|[12][0-9]|3[01])"),
        ExpressionUtcDatePositions: { Year: '$1', Month: '$2', Day: '$3' },
        ExpressionUtcTime: new RegExp("([01][0-9]|[2][0123]):([012345][0-9]):([012345][0-9])"),
        ExpressionUtcMs: new RegExp("\.([0-9]+)"),
        ExpressionZone: new RegExp("([+-])([01][0-9]|[2][0123]):([012345][0-9])"),
        ExpressionUtc: new RegExp(new RegExp("([0-9][0-9][0-9][0-9])-(0[1-9]|1[012])-(0[1-9]|[12][0-9]|3[01])").toString() + "[T ]" + new RegExp("([01][0-9]|[2][0123]):([012345][0-9]):([012345][0-9])").toString()),
        Expressions:
        {
            Default: new RegExp("(0[1-9]|1[012])/(0[1-9]|[12][0-9]|3[01])/([0-9][0-9])"),
            UtcDate: new RegExp("([0-9][0-9][0-9][0-9])-(0[1-9]|1[012])-(0[1-9]|[12][0-9]|3[01])"),
            UtcTime: new RegExp("([01][0-9]|[2][0123]):([012345][0-9]):([012345][0-9])"),
            UtcMs: new RegExp("\.([0-9]+)"),
            Zone: new RegExp("([+-])([01][0-9]|[2][0123]):([012345][0-9])"),
            Utc: new RegExp("([0-9][0-9][0-9][0-9])-(0[1-9]|1[012])-(0[1-9]|[12][0-9]|3[01])" + "[T ]" + "([01][0-9]|[2][0123]):([012345][0-9]):([012345][0-9])")
        }
    }
};

SadaCore.DateTime.Now.ToString = function (format) {
    var currentDate = new SadaCore.DateTime;
    return currentDate.ToString(format);
};
SadaCore.DateTime.Expressions =
{
    Default: SadaCore.DateTime.Globalization[SadaCore.Language].Expressions.Default,
    UtcDate: SadaCore.DateTime.Globalization[SadaCore.Language].Expressions.UtcDate,
    UtcTime: SadaCore.DateTime.Globalization[SadaCore.Language].Expressions.UtcTime,
    UtcMs: SadaCore.DateTime.Globalization[SadaCore.Language].Expressions.UtcMs,
    Zone: SadaCore.DateTime.Globalization[SadaCore.Language].Expressions.Zone,
    Utc: SadaCore.DateTime.Globalization[SadaCore.Language].Expressions.Utc
};
SadaCore.DateTime.Expression = SadaCore.DateTime.Globalization[SadaCore.Language].Expression;
SadaCore.DateTime.ExpressionUtcDate = SadaCore.DateTime.Globalization[SadaCore.Language].ExpressionUtcDate;
SadaCore.DateTime.ExpressionUtcDatePositions = SadaCore.DateTime.Globalization[SadaCore.Language].ExpressionUtcDatePositions;
SadaCore.DateTime.ExpressionUtcTime = SadaCore.DateTime.Globalization[SadaCore.Language].ExpressionUtcTime;
SadaCore.DateTime.ExpressionUtcMs = SadaCore.DateTime.Globalization[SadaCore.Language].ExpressionUtcMs;
SadaCore.DateTime.ExpressionZone = SadaCore.DateTime.Globalization[SadaCore.Language].ExpressionZone;
SadaCore.DateTime.ExpressionUtc = SadaCore.DateTime.Globalization[SadaCore.Language].ExpressionUtc;

SadaCore.DateTime.SubtractDays = function (days, round) {
    date = this;
    var newDate = new Date(date - new SadaCore.TimeSpan(days, 0, 0, 0, 0).Ticks);
    // crop hours, minutes seconds.
    var nDate = newDate;
    if (round) {
        nDate = new Date(newDate.getFullYear(), newDate.getMonth(), newDate.getDate());
    }
    return nDate;
}

SadaCore.DateTime.SubtractMonths = function (months, round) {
    date = this;
    var totalMonths = (date.getFullYear() * 12) + (date.getMonth());
    totalMonths = totalMonths - months;
    var newYear = Math.floor((totalMonths) / 12);
    var newMonth = totalMonths - newYear * 12;
    date.setFullYear(newYear);
    date.setMonth(newMonth);
    var newDate = date;

    if (round) {
        newDate = new Date(date.getFullYear(), date.getMonth(), date.getDate());
    }
    return newDate;
}

SadaCore.DateTime.GetFromString = function (dateString, ignoreTimeZoneAndParseAsUtc) {
    date = this;
    var yyyy = 0;
    var MM = 0;
    var dd = 0;
    var dateMatch = dateString.match(SadaCore.DateTime.ExpressionUtcDate);
    if (dateMatch) {
        yyyy = dateMatch[0].replace(SadaCore.DateTime.ExpressionUtcDate, SadaCore.DateTime.ExpressionUtcDatePositions.Year);
        MM = dateMatch[0].replace(SadaCore.DateTime.ExpressionUtcDate, SadaCore.DateTime.ExpressionUtcDatePositions.Month);
        dd = dateMatch[0].replace(SadaCore.DateTime.ExpressionUtcDate, SadaCore.DateTime.ExpressionUtcDatePositions.Day);
    }
    var hh = 0;
    var mm = 0;
    var ss = 0;
    var timeMatch = dateString.match(SadaCore.DateTime.ExpressionUtcTime);
    if (timeMatch) {
        hh = timeMatch[0].replace(SadaCore.DateTime.ExpressionUtcTime, "$1");
        mm = timeMatch[0].replace(SadaCore.DateTime.ExpressionUtcTime, "$2");
        ss = timeMatch[0].replace(SadaCore.DateTime.ExpressionUtcTime, "$3");
    }
    var fff = 0;
    var msMatch = dateString.match(SadaCore.DateTime.ExpressionUtcMs);
    if (msMatch) {
        fff = msMatch[0].replace(SadaCore.DateTime.ExpressionUtcMs, "$1");
        fff = parseFloat("0." + fff);
        fff = parseInt(fff * 1000);
    }
    var znMatch = dateString.match(SadaCore.DateTime.ExpressionZone);
    var zn = 0;
    var zh = 0;
    var zm = 0;
    if (znMatch) {
        zn = parseInt(parseFloat(znMatch[0].replace(SadaCore.DateTime.ExpressionZone, "$1") + "1"));
        zh = parseInt(parseFloat(znMatch[0].replace(SadaCore.DateTime.ExpressionZone, "$2")) * zn);
        zm = parseInt(parseFloat(znMatch[0].replace(SadaCore.DateTime.ExpressionZone, "$3")) * zn);
    }
    if (ignoreTimeZoneAndParseAsUtc) {
        date.setUTCFullYear(yyyy, MM - 1, dd);
        date.setUTCHours(hh, mm, ss, fff);
    }
    else {
        var zeroZone = false;
        zeroZone = (zeroZone || (dateString.indexOf("GMT") > -1));
        zeroZone = (zeroZone || (dateString.indexOf("Z") > -1));

        if (zn == 0 && !zeroZone) {
            date.setFullYear(yyyy, MM - 1, dd);
            date.setHours(hh, mm, ss, fff);
        }
        else {
            date.setUTCFullYear(yyyy, MM - 1, dd);
            date.setUTCHours(hh, mm, ss, fff);

            date = new Date(date.getTime() - (zh * 60 + zm) * 60 * 1000);
        }
    }

    return date;
}

SadaCore.DateTime.GetFromUtcString = function (dateString) {

    date = this;
    date.GetFromString(dateString, true);
    return date;
}


SadaCore.DateTime.ToString = function (format) {

}

SadaCore.DateTime.ToString = function (dateTime, format) {
    var date;
    var format;
    switch (arguments.length) {
        case 0:
            date = this;
            format = date.DefaultFormat;
            break;
        case 1:
            date = this;
            format = arguments[0];
            break;
        case 2:
            date = arguments[0];
            format = arguments[1];
            break;
        default:
            return "";
            break
    }
    date.addZero = function (number) { return (number < 10) ? '0' + number : number };

    var wwwArray = SadaCore.DateTime.Globalization[SadaCore.Language].www;
    var dddArray = SadaCore.DateTime.Globalization[SadaCore.Language].ddd;
    var ddddArray = SadaCore.DateTime.Globalization[SadaCore.Language].dddd;
    var MMMArray = SadaCore.DateTime.Globalization[SadaCore.Language].MMM;
    var MMMMArray = SadaCore.DateTime.Globalization[SadaCore.Language].MMMM;
    if (format == null) { format = date.DefaultFormat };

    if (format == "Outlook") {
        var now = new Date();
        if (date.getFullYear() == now.getFullYear()
            && date.getMonth() == now.getMonth()
            && date.getDate() == now.getDate()) {
            results = "ddd HH:mm";
        } else {
            format = SadaCore.DateTime.Globalization[SadaCore.Language].OutlookFormat;
        }
    }
    if (format == "X") { format = SadaCore.DateTime.Globalization[SadaCore.Language].XFormat };

    var fff = date.getMilliseconds();
    var yyyy = date.getFullYear();
    var yy = new String(date.addZero(yyyy));
    yy = yy.substr(yy.length - 2, 2);
    var www = wwwArray[date.getDay()]; // Outdated!!!
    var dddd = ddddArray[date.getDay()];
    var ddd = dddArray[date.getDay()];
    var dd = date.addZero(date.getDate());
    var MMMM = MMMMArray[date.getMonth()];
    var MMM = MMMArray[date.getMonth()];
    var MM = date.addZero(date.getMonth() + 1);
    var hAmPm = date.getHours() % 12;
    if (hAmPm == 0) hAmPm = 12;
    var hh = date.addZero(hAmPm); // 12 format
    var HH = date.addZero(date.getHours()); // 24 format
    var mm = date.addZero(date.getMinutes());
    var ss = date.addZero(date.getSeconds());
    var tt = (date.getHours() < 12) ? "AM" : "PM";
    var zzz = date.addZero(date.getTimezoneOffset());
    var offset = date.getTimezoneOffset();
    var negative = (offset < 0);
    if (negative) offset = offset * -1;
    zzz = date.addZero(Math.floor(offset / 60)) + ":" + date.addZero((offset % 60));
    if (negative || offset == 0) {
        zzz = "+" + zzz;
    } else {
        zzz = "-" + zzz;
    }

    var strDate = new String(format);
    strDate = strDate.replace("yyyy", yyyy);
    strDate = strDate.replace("yy", yy);
    strDate = strDate.replace("www", www);
    strDate = strDate.replace("dddd", dddd);
    strDate = strDate.replace("ddd", ddd);
    strDate = strDate.replace("dd", dd);
    strDate = strDate.replace("MMMM", MMMM);
    strDate = strDate.replace("MMM", MMM);
    strDate = strDate.replace("MM", MM);
    strDate = strDate.replace("ss", ss);
    strDate = strDate.replace("hh", hh);
    strDate = strDate.replace("HH", HH);
    strDate = strDate.replace("mm", mm);
    strDate = strDate.replace("ss", ss);
    strDate = strDate.replace("tt", tt);
    strDate = strDate.replace("ffffff", (fff + "000000").substr(0, 6));
    strDate = strDate.replace("fff", (fff + "000").substr(0, 3));
    strDate = strDate.replace("zzz", zzz);
    return strDate;
}
SadaCore.DateTime.ToUtcString = function (format) {
    var offset = this.getTime() + (this.getTimezoneOffset() * 60000);
    var ss = new Date(offset);
    return ss.toString(format);
}
SadaCore.DateTime.ToDifferenceString = function (dateOld, dateNew) {
    this.addZero = function (number) { return (number < 10) ? '0' + number : number };
    dateNew = dateNew ? dateNew : new Date();
    var ms = dateNew.getTime() - dateOld.getTime();
    var nd = new Date(ms);
    var ph = nd.getHours();
    var pm = nd.getMinutes();
    var ps = nd.getSeconds();
    var msPassed = 1000 * (60 * (60 * ph + pm) + ps) + nd.getMilliseconds();
    var d = (nd.getTime() - msPassed) / 24 / 60 / 60 / 1000;
    var results = Math.round(d) + "d " + ph + "h " + pm + "m";
    return results;
}
SadaCore.DateTime.GetDayType = function (d, trimResults) {
    d = (d) ? d : new Date();
    var results = new String();
    if (d.getMonth() == 9 && d.getDate() == 31) results = "Halloween";
    if (d.getMonth() == 11 && d.getDate() == 31) results = "New Year";
    if (trimResults) {
        results = results.replace(" ", "");
    }
    return results;
}

SadaCore.DateTime.Separator = SadaCore.DateTime.Globalization[SadaCore.Language].Separator;
SadaCore.DateTime.YearMin = SadaCore.DateTime.Globalization[SadaCore.Language].YearMin;
SadaCore.DateTime.YearMax = SadaCore.DateTime.Globalization[SadaCore.Language].YearMax;
SadaCore.DateTime.DateFormat = SadaCore.DateTime.Globalization[SadaCore.Language].DateFormat;
SadaCore.DateTime.Expression = SadaCore.DateTime.Globalization[SadaCore.Language].Expression;

SadaCore.DateTime.StripCharsInBag = function (s, bag) {
    var returnString = "";
    for (var i = 0; i < s.length; i++) {
        var c = s.charAt(i);
        if (bag.indexOf(c) == -1) returnString += c;
    }
    return returnString;
}

SadaCore.DateTime.DaysInFebruary = function (valYear) {
    return (((valYear % 4 == 0) && ((!(valYear % 100 == 0)) || (valYear % 400 == 0))) ? 29 : 28);
}

SadaCore.DateTime.DaysArray = function (valYear) {
    var arrDays = new Array;
    for (var i = 1; i <= 12; i++) {
        arrDays[i] = 31;
        if (i == 4 || i == 6 || i == 9 || i == 11) { arrDays[i] = 30 };
    }
    // Set February days.
    arrDays[2] = SadaCore.DateTime.DaysInFebruary(valYear);
    return arrDays;
}

SadaCore.DateTime.IsDate = function (valDate) {

    var dateString = new String(valDate);
    results = "";

    if (!SadaCore.DateTime.Expression.test(dateString)) return "Invalid! <span style=\"color: gray;\">Format: mm/dd/yyyy</span>";

    var MM = parseInt(dateString.replace(SadaCore.DateTime.Expression, "$1"), 10);
    var DD = parseInt(dateString.replace(SadaCore.DateTime.Expression, "$2"), 10);
    var YY = parseInt(dateString.replace(SadaCore.DateTime.Expression, "$3"), 10);

    if (YY >= 0 && YY <= 50) YY += 2000;
    if (YY > 50 && YY <= 99) YY += 1900;
    var DaysInMonth = SadaCore.DateTime.DaysArray(YY)[MM];

    if (MM < 1 || MM > 12) return "Invalid Month";
    if (DD > DaysInMonth) return "Invalid Day";
    if (YY < SadaCore.DateTime.YearMin || YY > SadaCore.DateTime.YearMax) return "Invalid Year";
    return results;
}

Date.prototype.GetFromString = SadaCore.DateTime.GetFromString;
Date.prototype.GetFromUtcString = SadaCore.DateTime.GetFromUtcString;
Date.prototype.DefaultFormat = SadaCore.DateTime.Globalization[SadaCore.Language].DateFormat;
Date.prototype.toString = SadaCore.DateTime.ToString;
Date.prototype.ToString = SadaCore.DateTime.ToString;
Date.prototype.toUtcString = SadaCore.DateTime.ToUtcString;

SadaCore.BaseEnum = {

    Undefined: 0,
    ToString: function (value) {

        for (var item in this) {

            if (!isNaN(this[item]) && this[item] == value) {

                return item;
            }
        }
        return 'Undefined';
    },
    GetByName: function (itemName) {

        if (this[itemName]) {

            return this[itemName];
        }

        return this.Undefined;
    },
    GetNames: function () {

        var names = [];

        for (var item in this) {

            if (!isNaN(this[item])) {

                names.push(item);
            }
        }

        return names;
    },
    IsDefined: function (value) {

        return this[value] != undefined && !isNaN(this[value]);
    },
    Parse: function (itemValue) {

        var value = this.GetByValue(itemValue);

        if (value == this.Undefined) {

            value = this.GetByName(itemValue);
        }

        return value;
    },
    IsFlagOn: function (targetVal, checkVal) {

        return (targetVal & checkVal) == checkVal;
    }
};

SadaCore.RegisterNameSpace("SadaCore.UI");

SadaCore.UI = {

    AutoFocusEnabled: true,
    SetFocusOnFirstElement: function () {

        try {
            if (SadaCore.UI.AutoFocusEnabled) {

                var body = SadaCore.Elements.Body;
                var autoFocus = body.find(".autoFocus:first");
                if (!autoFocus.length) {
                    autoFocus = body.find(":text:first");
                }
                autoFocus.focus();
            }
        }
        catch (e) {
            SadaCore.HandleException(e.message, "SadaCore.UI.SetFocusOnFirstElement");
        }
    }
};

/* json2.js 
* 2008-01-17
* Public Domain
* No warranty expressed or implied. Use at your own risk.
* See http://www.JSON.org/js.html
*/

SadaCore.RegisterNameSpace("JSON");

//if (!this.JSON) 
{
    SadaCore.JSONOrginal = function () {

        function f(n) { return n < 10 ? '0' + n : n; }
        Date.prototype.toJSON = function () {
            return this.getUTCFullYear() + '-' +
f(this.getUTCMonth() + 1) + '-' +
f(this.getUTCDate()) + 'T' +
f(this.getUTCHours()) + ':' +
f(this.getUTCMinutes()) + ':' +
f(this.getUTCSeconds()) + 'Z';
        }; var m = { '\b': '\\b', '\t': '\\t', '\n': '\\n', '\f': '\\f', '\r': '\\r', '"': '\\"', '\\': '\\\\' }; function stringify(value, whitelist) {
            var a, i, k, l, r = /["\\\x00-\x1f\x7f-\x9f]/g, v; switch (typeof value) {
                case 'string': return r.test(value) ? '"' + value.replace(r, function (a) {
                    var c = m[a]; if (c) { return c; }
                    c = a.charCodeAt(); return '\\u00' + Math.floor(c / 16).toString(16) +
(c % 16).toString(16);
                }) + '"' : '"' + value + '"'; case 'number': return isFinite(value) ? String(value) : 'null'; case 'boolean': case 'null': return String(value); case 'object': if (!value) { return 'null'; }
                    if (typeof value.toJSON === 'function') { return stringify(value.toJSON()); }
                    a = []; if (typeof value.length === 'number' && !(value.propertyIsEnumerable('length'))) {
                        l = value.length; for (i = 0; i < l; i += 1) { a.push(stringify(value[i], whitelist) || 'null'); }
                        return '[' + a.join(',') + ']';
                    }
                    if (whitelist) { l = whitelist.length; for (i = 0; i < l; i += 1) { k = whitelist[i]; if (typeof k === 'string') { v = stringify(value[k], whitelist); if (v) { a.push(stringify(k) + ':' + v); } } } } else { for (k in value) { if (typeof k === 'string') { v = stringify(value[k], whitelist); if (v) { a.push(stringify(k) + ':' + v); } } } }
                    return '{' + a.join(',') + '}';
            }
        }
        return {
            stringify: stringify, parse: function (text, filter) {
                var j; function walk(k, v) {
                    var i, n; if (v && typeof v === 'object') { for (i in v) { if (Object.prototype.hasOwnProperty.apply(v, [i])) { n = walk(i, v[i]); if (n !== undefined) { v[i] = n; } } } }
                    return filter(k, v);
                }
                if (/^[\],:{}\s]*$/.test(text.replace(/\\./g, '@').replace(/"[^"\\\n\r]*"|true|false|null|-?\d+(?:\.\d*)?(?:[eE][+\-]?\d+)?/g, ']').replace(/(?:^|:|,)(?:\s*\[)+/g, ''))) { j = eval('(' + text + ')'); return typeof filter === 'function' ? walk('', j) : j; }
                throw new SyntaxError('parseJSON');
            }
        };
    }();

    JSON = $.extend(JSON, SadaCore.JSONOrginal);
}

SadaCore.RegisterNameSpace("SadaCore.UI");

SadaCore.UI = {
    ResourceType: $.extend({}, SadaCore.BaseEnum, {
        Image: 1,
        Video: 2,
        Audio: 4
    }),
    Config: {

        PopupWidth: 640,
        PopupHeight: 480,
        PopupScrolling: 'auto',
        PopupShowCloseButton: true
    },   
    Popup: {
        Data: {},
        Open: function (url, width, height, scrolling, showCloseButton, inCurrentWindow, isImage) {

            var popup = SadaCore.UI.Popup;

            try {
                width = typeof width == 'undefined' ? SadaCore.UI.Config.PopupWidth : width;
                height = typeof height == 'undefined' ? SadaCore.UI.Config.PopupHeight : height;
                scrolling = typeof scrolling == 'undefined' ? SadaCore.UI.Config.PopupScrolling : scrolling;
                showCloseButton = typeof showCloseButton == 'undefined' ? SadaCore.UI.Config.PopupShowCloseButton : showCloseButton;
                inCurrentWindow = typeof inCurrentWindow == 'undefined' ? true : inCurrentWindow;
                isImage = url.toLowerCase().endsWith(".jpg");

                var config = {
                    url: url,
                    type: isImage ? 'image' : 'iframe',
                    scrolling: scrolling,
                    autoDimensions: false,
                    width: width,
                    height: height,
                    showCloseButton: showCloseButton
                };

                popup.Data.CurrentConfig = config;

                if (!inCurrentWindow && self.parent) {

                    self.parent.SadaCore.UI.Popup.Data.OldConfig = self.parent.SadaCore.UI.Popup.Data.CurrentConfig;
                    self.parent.$.fancybox(url, config);
                }
                else {

                    self.parent.SadaCore.UI.Popup.Data.OldConfig = null;
                    $.fancybox(url, config);
                }
            }
            catch (e) {

                SadaCore.HandleException(e, "SadaCore.UI.Popup.Open");
            }
        },
        Close: function (redirectUrl) {

            try {

                var config = self.parent.SadaCore.UI.Popup.Data.OldConfig;
                self.parent.SadaCore.UI.Popup.Data.OldConfig = null;

                if (config) {

                    self.parent.SadaCore.UI.Popup.Open(config.url, config.width, config.height, config.scrolling, config.showCloseButton, true);
                }
                else {

                    self.parent.$.fancybox.close();

                    $.fancybox.close();

                    if (redirectUrl) {

                        self.parent.location.href = redirectUrl;
                    }
                }
            }
            catch (e) {

                SadaCore.HandleException(e, "SadaCore.UI.Popup.Close");
            }
        },
        OpenWindow: function (url) {

            window.open(url, "_blank");
        }
    }
};

