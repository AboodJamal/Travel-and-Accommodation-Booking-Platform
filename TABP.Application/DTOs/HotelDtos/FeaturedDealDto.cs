﻿namespace TABP.Application.DTOs.HotelDtos;

public record FeaturedDealDto
{
    public string CityName { get; set; }
    public Guid HotelId { get; set; }
    public string HotelName { get; set; }
    public float HotelRating { get; set; }
    public Guid RoomClassId { get; set; }
    public float BaseRoomPrice { get; set; }
    public float Discount { get; set; }
    public float FinalRoomPrice { get; set; }
}