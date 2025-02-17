namespace WebAGK.Shared.Abstractions.Entities;
public class StoredFile : EntityBase
{
	public string FileName { get; set; }
	public string FileDescription { get; set; }
	public string FileHash { get; set; }
	public string FileStoragePath { get; set; }
	public string FileStorageName { get; set; }
}
