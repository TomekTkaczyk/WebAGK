namespace WebAGK.Shared.Abstractions.Entities;

public interface IStoredFile : IEntityBase {
    string FileName { get; set; }
    string FileDescription { get; set; }
    string FileHash { get; set; }
    string FileStoragePath { get; set; }
    string FileStorageName { get; set; }
}