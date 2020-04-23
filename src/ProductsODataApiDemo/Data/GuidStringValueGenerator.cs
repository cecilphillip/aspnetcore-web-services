using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace ProductsODataApiDemo.Data
{
    public class GuidStringValueGenerator : ValueGenerator<string>
    {
        public override bool GeneratesTemporaryValues => false;

        public override string Next([NotNullAttribute] EntityEntry entry)
            => Guid.NewGuid().ToString("N");
    }
}