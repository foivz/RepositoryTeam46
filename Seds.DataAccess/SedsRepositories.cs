using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seds.DataAccess
{
    /// <summary>
    /// The "Unit of Work"
    ///     1) decouples the repos from the console,controllers,ASP.NET pages....
    ///     2) decouples the DbContext and EF from the controllers
    ///     3) manages the UoW
    /// </summary>
    /// <remarks>
    /// This class implements the "Unit of Work" pattern in which
    /// the "UoW" serves as a facade for querying and saving to the database.
    /// Querying is delegated to "repositories".
    /// Each repository serves as a container dedicated to a particular
    /// root entity type such as a applicant.
    /// A repository typically exposes "Get" methods for querying and
    /// will offer add, update, and delete methods if those features are supported.
    /// The repositories rely on their parent UoW to provide the interface to the
    /// data .
    /// </remarks>
    public class SedsRepositories : ISedsRepositories, IDisposable
    {
        public Entities DbContext { get; set; }

        public SedsRepositories()
        {
            CreateDbContext();
        }

        //repositories
        #region Repositries
        private IRepository<Certificate> _certifications;
        private IRepository<CertificateType> _certificateTypes;
        private IRepository<CourseGroup> _courseGroups;
        private IRepository<OrganizationalUnit> _organizationalUnits;
        private IRepository<Role> _roles;
        private IRepository<Student> _students;
        private IRepository<Student_Studies> _studentStudies;
        private IRepository<Study> _studies;
        private IRepository<StudyType> _studyTypes;
        private IRepository<User> _users;
        private IRepository<User_Roles> _userRoles;


        //get Certificate repo
        public IRepository<Certificate> Certificates
        {
            get
            {
                if (_certifications == null)
                {
                    _certifications = new Repository<Certificate>(DbContext);

                }
                return _certifications;
            }
        }

        public IRepository<CertificateType> CertificateTypes
        {
            get
            {
                if (_certificateTypes == null)
                {
                    _certificateTypes = new Repository<CertificateType>(DbContext);
                }
                return _certificateTypes;
            }
        }

        public IRepository<CourseGroup> CourseGroups
        {
            get
            {
                if (_courseGroups == null)
                {
                    _courseGroups = new Repository<CourseGroup>(DbContext);
                }
                return _courseGroups;
            }
        }

        public IRepository<OrganizationalUnit> OrganizationalUnits
        {
            get
            {
                if (_organizationalUnits == null)
                {
                    _organizationalUnits = new Repository<OrganizationalUnit>(DbContext);
                }
                return _organizationalUnits;
            }
        }

        public IRepository<Role> Roles
        {
            get
            {
                if (_roles == null)
                {
                    _roles = new Repository<Role>(DbContext);
                }
                return _roles;
            }
        }

        //get Student repo
        public IRepository<Student> Students
        {
            get
            {
                if (_students == null)
                {
                    _students = new Repository<Student>(DbContext);

                }
                return _students;
            }
        }

        public IRepository<Student_Studies> StudentStudies
        {
            get
            {
                if (_studentStudies == null)
                {
                    _studentStudies = new Repository<Student_Studies>(DbContext);
                }
                return _studentStudies;
            }
        }

        //get Study repo
        public IRepository<Study> Studies
        {
            get
            {
                if (_studies == null)
                {
                    _studies = new Repository<Study>(DbContext);

                }
                return _studies;
            }
        }

        public IRepository<StudyType> StudyTypes
        {
            get
            {
                if (_studyTypes == null)
                {
                    _studyTypes = new Repository<StudyType>(DbContext);
                }
                return _studyTypes;
            }
        }

        public IRepository<User> Users
        {
            get
            {
                if (_users == null)
                {
                    _users = new Repository<User>(DbContext);
                }
                return _users;
            }
        }
        public IRepository<User_Roles> UserRoles
        {
            get
            {
                if (_userRoles == null)
                {
                    _userRoles = new Repository<User_Roles>(DbContext);

                }
                return _userRoles;
            }
        }



        #endregion

        /// <summary>
        /// Save pending changes to the database
        /// </summary>
        public void SaveChanges()
        {
            DbContext.SaveChanges();
        }

        protected void CreateDbContext()
        {
            DbContext = new Entities();

            // Do NOT enable proxied entities, else serialization fails.
            //if false it will not get the associated certification and skills when we
            //get the applicants
            DbContext.Configuration.ProxyCreationEnabled = false;

            // Load navigation properties explicitly (avoid serialization trouble)
            DbContext.Configuration.LazyLoadingEnabled = false;

            // Because Web API will perform validation, we don't need/want EF to do so
            DbContext.Configuration.ValidateOnSaveEnabled = false;

            //DbContext.Configuration.AutoDetectChangesEnabled = false;
            // We won't use this performance tweak because we don't need 
            // the extra performance and, when autodetect is false,
            // we'd have to be careful. We're not being that careful.
        }

        #region IDisposable

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (DbContext != null)
                {
                    DbContext.Dispose();
                }
            }
        }

        #endregion
    }
}
