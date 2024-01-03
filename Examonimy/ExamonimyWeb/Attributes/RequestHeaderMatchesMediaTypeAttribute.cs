using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Primitives;
using Microsoft.Net.Http.Headers;

namespace ExamonimyWeb.Attributes;

[AttributeUsage(AttributeTargets.All, Inherited = true, AllowMultiple = true)]
public class RequestHeaderMatchesMediaTypeAttribute : Attribute, IActionConstraint
{

    private readonly string _requestHeaderToMatch;
    private readonly MediaTypeCollection _mediaTypes = new ();

    public RequestHeaderMatchesMediaTypeAttribute(string requestHeaderToMatch, string mediaType, params string[] otherMediaTypes)
    {
        _requestHeaderToMatch = requestHeaderToMatch ?? throw new ArgumentNullException(nameof(requestHeaderToMatch));

        // check if the request media types are valid then add them to the _mediaTypes collection
        if (MediaTypeHeaderValue.TryParse(mediaType, out var parsedMediaType))
            _mediaTypes.Add(parsedMediaType);
        else
            throw new ArgumentException(null, nameof(mediaType));

        foreach (var otherMediaType in otherMediaTypes)
        {
            if (MediaTypeHeaderValue.TryParse(otherMediaType, out var parsedOtherMediaType))
                _mediaTypes.Add(parsedOtherMediaType);
            else
                throw new ArgumentException(null, nameof(otherMediaType));
        }
    }

    public int Order { get; }

    public bool Accept(ActionConstraintContext context)
    {
        var requestHeaders = context.RouteContext.HttpContext.Request.Headers;

        if (!requestHeaders.TryGetValue(_requestHeaderToMatch, out var mediaType ) || StringValues.IsNullOrEmpty(mediaType))
            return false;

        var parsedMediaType = new MediaType(mediaType!);

        // if one of the media type matches, return true
        return _mediaTypes.Any(mt => parsedMediaType.Equals(new MediaType(mt)));

    }
}
