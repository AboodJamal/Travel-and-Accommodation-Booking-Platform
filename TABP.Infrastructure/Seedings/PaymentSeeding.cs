using TABP.Domain.Entities;
using Domain.Enums;

namespace Infrastructure.Common.Persistence.Seeding
{
    public class PaymentSeeding
    {
        public static IEnumerable<Payment> SeedData()
        {
            return new List<Payment>
            {
                new()
                {
                    Id = new Guid("ea5d0358-0ed8-4c16-8693-77d1c5f6f1e1"),
                    BookingId = new Guid("d4b8e2b5-4a47-4f7d-9571-1018a3e8745f"), // Correct BookingId
                    Method = PaymentMethod.CreditCard,
                    Status = PaymentStatus.Completed,
                    Amount = 1750.0
                },
                new()
                {
                    Id = new Guid("d9c0b7c4-4c8f-4a93-8785-d72011fdc17b"),
                    BookingId = new Guid("bbf9562b-3a0d-4729-a421-55e2a84f9a0d"), // Correct BookingId
                    Method = PaymentMethod.Cash,
                    Status = PaymentStatus.Cancelled,
                    Amount = 800.0
                },
                new()
                {
                    Id = new Guid("66e6cfe3-91d3-4266-bf42-e4bcb024c8a7"),
                    BookingId = new Guid("cd0a6077-c3a7-4d56-8356-12a6de4e7a82"), // Correct BookingId
                    Method = PaymentMethod.Cash,
                    Status = PaymentStatus.Pending,
                    Amount = 2200.0
                }
            };
        }
    }
}
