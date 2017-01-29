using System;
using System.Security.Cryptography;
using System.Text;
using System.Web.Security;
//using CustomMembershipEF.Contexts;
//using CustomMembershipEF.Entities;
using Hypnosis.Web.Data;
using System.Data.Entity.Core.Objects;
using System.Linq;


namespace CustomMembershipEF.Infrastructure
{
    public class CustomMembershipProvider : MembershipProvider
    {
        public override string ApplicationName
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {

            var args = new ValidatePasswordEventArgs(username, oldPassword, true);
            OnValidatingPassword(args);

            if (args.Cancel)
            {
              
                return false;
            }

            using (var usersContext = new HypnosisEntities())
            {
                var user = GetUser(usersContext, username);
                if (user == null)
                    return false;
                else
                {
                    string Password = GetMd5Hash(newPassword);
                    user.Password = Password;
                    usersContext.SaveChanges();

                }
            }
            return true;

        }

        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
        {
            var args = new ValidatePasswordEventArgs(username, password, true);
            OnValidatingPassword(args);

            if (args.Cancel)
            {
                status = MembershipCreateStatus.InvalidPassword;
                return null;
            }

            if (RequiresUniqueEmail && GetUserNameByEmail(email) != string.Empty)
            {
                status = MembershipCreateStatus.DuplicateEmail;
                return null;
            }

            var user = GetUser(username, true);

            if (user == null)
            {
             
                using (var usersContext = new HypnosisEntities())
                {
                    var userObj = new UserProfile { UserName = username, UserEmailAddress = email };
                    
                    string Password = GetMd5Hash(password);
                    userObj.Password = Password;
                    AddUser(usersContext, userObj);       
                   
                }

                status = MembershipCreateStatus.Success;

                return GetUser(username, true);
            }
            status = MembershipCreateStatus.DuplicateUserName;

            return null;
        }

        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            throw new NotImplementedException();
        }

        public override bool EnablePasswordReset
        {
            get { throw new NotImplementedException(); }
        }

        public override bool EnablePasswordRetrieval
        {
            get { throw new NotImplementedException(); }
        }

        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override int GetNumberOfUsersOnline()
        {
            throw new NotImplementedException();
        }

        public override string GetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser GetUser(string username, bool userIsOnline)
        {
            var usersContext = new HypnosisEntities();
            var user = GetUser(usersContext,username);
            if (user != null)
            {
                var memUser = new MembershipUser("CustomMembershipProvider", username, user.UserId, user.UserEmailAddress,
                                                            string.Empty, string.Empty,
                                                            true, false, DateTime.MinValue,
                                                            DateTime.MinValue,
                                                            DateTime.MinValue,
                                                            DateTime.Now, DateTime.Now);
                return memUser;
            }
            return null;
        }



        public void AddUser(HypnosisEntities usersContext,UserProfile user)
        {
            usersContext.UserProfiles.Add(user);
            usersContext.SaveChanges();

           
        }

        public UserProfile GetUser(HypnosisEntities usersContext,string userName)
        {

            var user=usersContext.UserProfiles.SingleOrDefault(u => u.UserName == userName);  
                
            return user;
        }

        public UserProfile GetUser(HypnosisEntities usersContext,string userName, string password)
        {
            var user = usersContext.UserProfiles.SingleOrDefault(u => u.UserName == userName && u.Password == password);
            return user;
        }
        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            throw new NotImplementedException();
        }

        public override string GetUserNameByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public override int MaxInvalidPasswordAttempts
        {
            get { throw new NotImplementedException(); }
        }

        public override int MinRequiredNonAlphanumericCharacters
        {
            get { throw new NotImplementedException(); }
        }

        public override int MinRequiredPasswordLength
        {
            get { return 6; }
        }

        public override int PasswordAttemptWindow
        {
            get { throw new NotImplementedException(); }
        }

        public override MembershipPasswordFormat PasswordFormat
        {
            get { throw new NotImplementedException(); }
        }

        public override string PasswordStrengthRegularExpression
        {
            get { throw new NotImplementedException(); }
        }

        public override bool RequiresQuestionAndAnswer
        {
            get { throw new NotImplementedException(); }
        }

        public override bool RequiresUniqueEmail
        {
            get { return false; }
        }

        public override string ResetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override bool UnlockUser(string userName)
        {
            throw new NotImplementedException();
        }

        public override void UpdateUser(MembershipUser user)
        {
            throw new NotImplementedException();
        }

        public override bool ValidateUser(string username, string password)
        {
            var md5Hash = GetMd5Hash(password);

            using (var usersContext = new HypnosisEntities())
            {
                var requiredUser = GetUser(usersContext,username, md5Hash);
                return requiredUser != null;
            }
        }

        public static string GetMd5Hash(string value)
        {
            var md5Hasher = MD5.Create();
            var data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(value));
            var sBuilder = new StringBuilder();
            for (var i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }
    }
}