﻿namespace Restaurant.Notifications.Enums;

[Flags]
public enum Accepted
{
    Rejected = 0,
    Kitchen = 1,
    Booking = 2,
    All = Kitchen | Booking
}
