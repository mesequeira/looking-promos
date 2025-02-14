﻿namespace LookingPromos.WebApi.Infrastructure.MessageBroker.Options;

internal sealed class MessageBrokerOptions
{
    public required string ConnectionString { get; set; }
    public required string Username { get; set; }
    public required string Password { get; set; }
}