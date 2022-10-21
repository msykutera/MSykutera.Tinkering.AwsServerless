using System.Net;
using System.Text.Json;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;
using Microsoft.Extensions.Options;
using MSykutera.Tinkering.AwsServerless.Dtos;
using MSykutera.Tinkering.AwsServerless.Settings;

namespace MSykutera.Tinkering.AwsServerless.Repositories
{
    public class PostRepository : IRepository<PostDto>
    {
        private readonly IAmazonDynamoDB _client;

        private readonly IOptions<DatabaseSettings> _databaseSettings;

        public PostRepository(IAmazonDynamoDB client, IOptions<DatabaseSettings> databaseSettings)
        {
            _client = client;
            _databaseSettings = databaseSettings;
        }

        public async Task<PostDto?> GetAsync(int id)
        {
            var request = new GetItemRequest
            {
                TableName = _databaseSettings.Value.TableName,
                Key = new Dictionary<string, AttributeValue> { { "Id", new AttributeValue(id.ToString()) } }
            };
            var response = await _client.GetItemAsync(request);

            if (response.Item.Count == 0) return null;

            var itemAsDocument = Document.FromAttributeMap(response.Item);
            var result = JsonSerializer.Deserialize<PostDto>(itemAsDocument);
            return result;
        }

        public async Task<bool> CreateAsync(PostDto post)
        {
            var postAsJson = JsonSerializer.Serialize(post);
            var postAsDocument = Document.FromJson(postAsJson);
            var postAsAttribute = postAsDocument.ToAttributeMap();
            var request = new PutItemRequest
            {
                TableName = _databaseSettings.Value.TableName,
                Item = postAsAttribute
            };
                
            var response = await _client.PutItemAsync(request);
            return response.HttpStatusCode == HttpStatusCode.OK;
        }

        public async Task<bool> UpdateAsync(PostDto post)
        {
            var postAsJson = JsonSerializer.Serialize(post);
            var postAsDocument = Document.FromJson(postAsJson);
            var postAsAttribute = postAsDocument.ToAttributeMap();
            var request = new PutItemRequest
            {
                TableName = _databaseSettings.Value.TableName,
                Item = postAsAttribute
            };

            var response = await _client.PutItemAsync(request);
            return response.HttpStatusCode == HttpStatusCode.OK;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var request = new DeleteItemRequest
            {
                TableName = _databaseSettings.Value.TableName,
                Key = new Dictionary<string, AttributeValue> { { "Id", new AttributeValue(id.ToString()) } }
            };
            var response = await _client.DeleteItemAsync(request);
            return response.HttpStatusCode == HttpStatusCode.OK;
        }
    }
}
