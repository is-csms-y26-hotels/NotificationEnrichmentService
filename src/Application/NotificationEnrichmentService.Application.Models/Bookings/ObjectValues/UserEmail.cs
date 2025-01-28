﻿using System.Text.RegularExpressions;

namespace NotificationEnrichmentService.Application.Models.Bookings.ObjectValues;

public readonly partial record struct UserEmail
{
    public string Value { get; private init; }

    public UserEmail(string value)
    {
        Match match = EmailRegex().Match(value);

        if (!match.Success) throw new ArgumentException($"Invalid email format: {value}");

        Value = match.Value;
    }

    [GeneratedRegex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$")]
    private static partial Regex EmailRegex();
}