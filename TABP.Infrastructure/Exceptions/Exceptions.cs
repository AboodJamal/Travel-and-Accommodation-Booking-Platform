namespace Infrastructure.Exceptions;

    public abstract class DomainException : Exception
    {
        protected DomainException(string message) : base(message)
        {
        }
    }

    public class BookingCheckInDateException : DomainException
    {
        public BookingCheckInDateException()
            : base("Can't delete a booking which is in the past.")
        {
        }
    }

    public class ConstraintViolationException : DomainException
    {
        public ConstraintViolationException(string message)
            : base(message)
        {
        }
    }

    public class InvalidDiscountDateException : DomainException
    {
        public InvalidDiscountDateException(string message)
            : base(message)
        {
        }
    }

    public class NotFoundException : DomainException
    {
        public string EntityName { get; }

        public NotFoundException(string entityName, string message = null)
            : base(message ?? $"{entityName} not found.")
        {
            EntityName = entityName;
        }
    }

    public class UserExistsException : DomainException
    {
        public string Username { get; }

        public UserExistsException(string username)
            : base($"The user '{username}' already exists.")
        {
            Username = username;
        }
    }

