using System;
using System.Collections.Generic;

namespace StarApi.Model;

public partial class Audit
{
    public int Id { get; set; }

    public string TableName { get; set; } = null!;

    public Guid? RowId { get; set; }

    public string? OldValue { get; set; }

    public string? NewValue { get; set; }

    public string? OperationType { get; set; }

    public DateTime? Timestamp { get; set; }
}
