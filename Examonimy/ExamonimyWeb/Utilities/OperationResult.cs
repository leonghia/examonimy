﻿namespace ExamonimyWeb.Utilities
{
    public class OperationResult
    {
        public ICollection<OperationError>? Errors { get; set; }
        public bool Succeeded { get; set; } = true;
    }
}
