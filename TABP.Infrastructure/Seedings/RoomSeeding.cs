using TABP.Domain.Entities;
using System;
using System.Collections.Generic;

namespace Infrastructure.Common.Persistence.Seeding
{
    public class RoomSeeding
    {
        public static IEnumerable<Room> SeedData()
        {
            return new List<Room>
            {
                new()
                {
                    Id = new Guid("b362b1ae-4f39-453f-b0f3-5a8f9d1b2815"),
                    RoomTypeId = new Guid("f67a1832-c747-4bfe-946f-9b941d1059b3"), // Match with a valid RoomTypeId from RoomTypeSeeding
                    AdultsCapacity = 2,
                    ChildrenCapacity = 1,
                    View = "Garden View",
                    Rating = 4.7f
                },
                new()
                {
                    Id = new Guid("aa08b4e7-cbbc-4661-9bc3-2b2333bfe4de"),
                    RoomTypeId = new Guid("48a98ac1-9079-413a-8cc2-299a6c8a4515"), // Match with a valid RoomTypeId from RoomTypeSeeding
                    AdultsCapacity = 3,
                    ChildrenCapacity = 2,
                    View = "Sea View",
                    Rating = 4.3f
                },
                new()
                {
                    Id = new Guid("8ac05d5d-f8d9-49de-bf2d-2746763b1459"),
                    RoomTypeId = new Guid("cc8eb9be-0398-4a0d-bb6f-7ea5d58a9cf0"), // Match with a valid RoomTypeId from RoomTypeSeeding
                    AdultsCapacity = 4,
                    ChildrenCapacity = 2,
                    View = "Lake View",
                    Rating = 4.6f
                }
            };
        }
    }
}
