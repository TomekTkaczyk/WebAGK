using WebAGK.Shared.Abstractions.Entities;

namespace WebAGK.Shared.Infrastructure.Entities;
public class StoredFile : EntityBase, IStoredFile {
	public string FileName { get; set; }
	public string FileDescription { get; set; }
	public string FileHash { get; set; }
	public string FileStoragePath { get; set; }
	public string FileStorageName { get; set; }
}
