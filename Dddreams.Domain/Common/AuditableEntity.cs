﻿namespace Dddreams.Domain.Common;

public class AuditableEntity
{
    public DateTime CreatedDate { get; set; }
    public DateTime ModifiedDate { get; set; }
}