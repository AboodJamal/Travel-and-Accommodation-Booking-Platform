﻿namespace Infrastructure.ExtraModels;

public class ErrorResponse
{
    public List<ErrorModel> Errors { get; set; } = new List<ErrorModel>();
}