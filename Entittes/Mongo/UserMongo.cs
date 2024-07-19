using MongoDB.Bson.Serialization.Attributes;

namespace Entittes.Mongo;

public class UserMongo
{
    [BsonId]
    public Guid Id { get; set; }

    [BsonElement("name")]
    public string Name { get; set; }

    public string Family { get; set; }

    [BsonIgnore]
    public string FullName => $"{Name} {Family}";
}
