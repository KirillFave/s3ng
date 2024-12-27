// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace DeliveryService.Models;

public class BaseEntity
{ 
    public Guid Guid { get; set; }

    public BaseEntity()
    {
        Guid = Guid.NewGuid();
    }
}
