﻿namespace TABP.Application.DTOs.DiscountDtos;

public record DiscountCreationDto
{
    public Guid RoomType { get; set; }
    public float DiscountPercentage { get; set; }
    public DateTime FromDate { get; set; }
    public DateTime ToDate { get; set; }
}