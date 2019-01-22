using System;

namespace SimpleChallenge
{
    public class Challenge
    {
        public abstract class BaseUser
        {
            private string _name;
            private DateTime _lastLoginDate;
            private bool _isLoggedIn;

            public BaseUser(string name)
            {
                this._name = name;
            }

            public string GetName()
            {
                return this._name;
            }

            public void SetName(string name)
            {
                this._name = name;
            }

            public virtual bool CanAdd(Comment comment)
            {
                return true;
            }

            public virtual bool CanEdit(Comment comment)
            {
                var author = comment.GetAuthor();
                if(comment.GetAuthor().GetName().Equals(this.GetName()))
                {
                    return true;
                }

                return false;
            }
            public virtual bool CanDelete(Comment comment)
            {
                return false;
            }

            public bool IsLoggedIn()
            {
                return this._isLoggedIn;
            }

            public DateTime GetLastLoggedInAt()
            {
                return this._lastLoginDate;
            }

            public void LogIn()
            {
                this._isLoggedIn = true;
                this._lastLoginDate = DateTime.Now;
            }

            public void LogOut()
            {
                this._isLoggedIn = false;
            }
        }

        public class User : BaseUser
        {
            public User(string name) : base(name) { }
        }

        public class Moderator : BaseUser
        {
            public Moderator(string name) : base(name) { }

            public override bool CanAdd(Comment comment)
            {
                return false;
            }
            public override bool CanEdit(Comment comment)
            {
                return false;
            }

            public override bool CanDelete(Comment comment)
            {
                return true;
            }
        }

        public class Admin : BaseUser
        {
            public Admin(string name) : base(name) { }

            public override bool CanAdd(Comment comment)
            {
                return true;
            }
            public override bool CanEdit(Comment comment)
            {
                return true;
            }

            public override bool CanDelete(Comment comment)
            {
                return true;
            }
        }

        public class Comment
        {
            private readonly BaseUser _author;
            private string _message;
            private readonly Comment _repliedTo;

            public Comment(BaseUser author, string message, Comment repliedTo)
            {
                this._author = author;
                this._message = message;
            }

            public string GetMessage()
            {
                return this._message;
            }
            public void SetMessage(string message)
            {
                if (this._author.CanAdd(this))
                {
                    this._message = message;
                }

                throw new Exception("This user cant add messages");
            }

            public DateTime GetCreatedAt()
            {
                return DateTime.Now;
            }

            public BaseUser GetAuthor()
            {
                return this._author;
            }

            public Comment GetRepliedTo()
            {
                return this._repliedTo;
            }

            public override string ToString()
            {
                return this._message;
            }
        }
    }
}



