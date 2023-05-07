using System;

internal sealed class RoleInfo
{
    public String RoleName { get; set; }
    public String RoleAge { get; set; }

    public override string ToString()
    {
        return $"RoleName:{RoleName},RoleAge:{RoleAge}";
    }
}