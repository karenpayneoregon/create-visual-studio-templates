# About

# Requires 

ConfigurationLibrary package under C:\OED\Dotnetland\NuGetLocal\ConfigurationLibrary.1.0.1.nupkg

# Connection string examples


**Standard**

```csharp
protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
{
    if (!optionsBuilder.IsConfigured)
    {
        DbContextConnections.NoLogging(optionsBuilder);
    }
}
```

**With logging sensitive data** 

To Debug.WriteLine which appears in the IDE Output window

```csharp
protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
{
    if (!optionsBuilder.IsConfigured)
    {
        DbContextConnections.StandardLogging(optionsBuilder);
    }
}
```

**Log to file**

Write to log.txt in application folder

```csharp
protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
{
    if (!optionsBuilder.IsConfigured)
    {
        DbContextConnections.CustomLogging(optionsBuilder);
    }
}
```