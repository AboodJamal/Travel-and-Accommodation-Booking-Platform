    using TABP.Domain.Entities;
    using Domain.Enums;
    using System;
    using System.Collections.Generic;

    namespace Infrastructure.Common.Persistence.Seeding
    {
        public class RoomTypeSeeding
        {
            public static IEnumerable<RoomType> SeedData()
            {
                return new List<RoomType>
                {
                    new()
                    {
                        Id = new Guid("f67a1832-c747-4bfe-946f-9b941d1059b3"),
                        HotelId = new Guid("b2e04d28-78c5-404d-9264-215f88e6b3a1"),
                        Category = RoomCategory.Single,
                        PricePerNight = 120.0f
                    },
                    new()
                    {
                        Id = new Guid("48a98ac1-9079-413a-8cc2-299a6c8a4515"),
                        HotelId = new Guid("a42d4d56-865b-4526-9a45-c8d5d8da3e6f"),
                        Category = RoomCategory.Double,
                        PricePerNight = 180.0f
                    },
                    new()
                    {
                        Id = new Guid("cc8eb9be-0398-4a0d-bb6f-7ea5d58a9cf0"),
                        HotelId = new Guid("572f85b1-6223-442a-bf0a-7dbb9307f1d7"),
                        Category = RoomCategory.Single,
                        PricePerNight = 300.0f
                    }
                };
            }
        }
    }
