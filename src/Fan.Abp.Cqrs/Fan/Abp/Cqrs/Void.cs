using System;
using System.Threading.Tasks;

namespace Fan.Abp.Cqrs
{
    public readonly struct Void : IEquatable<Void>, IComparable<Void>, IComparable
    {
        private static readonly Void _value = new();

        public static ref readonly Void Value => ref _value;

        public static Task<Void> Task { get; } = System.Threading.Tasks.Task.FromResult(_value);

        public int CompareTo(Void other) => 0;

        int IComparable.CompareTo(object? obj) => 0;

        public override int GetHashCode() => 0;

        public bool Equals(Void other) => true;

        public override bool Equals(object? obj) => obj is Void;

        public static bool operator ==(Void first, Void second) => true;

        public static bool operator !=(Void first, Void second) => false;

        public override string ToString() => "()";
    }
}