﻿namespace Common.Response;

public class ServiceSubStatus
{
    public string ErrorMessage { get; set; }
    public string Subject { get; set; }
    public int StatusCode { get; set; }
}

public class ServiceException
{
    public string ErrorMessage { get; set; }
    public int StatusCode { get; set; }
}