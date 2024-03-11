namespace GarageGroup.Infra;

public enum HttpFailureCode
{
    Unknown,

    Continue = 100,

    SwitchingProtocols = 101,

    Processing = 102,

    EarlyHints = 103,

    MultipleChoices = 300,

    MovedPermanently = 301,

    Found = 302,

    SeeOther = 303,

    NotModified = 304,

    UseProxy = 305,

    Unused = 306,

    TemporaryRedirect = 307,

    PermanentRedirect = 308,

    BadRequest = 400,

    Unauthorized = 401,

    PaymentRequired = 402,

    Forbidden = 403,

    NotFound = 404,

    MethodNotAllowed = 405,

    NotAcceptable = 406,

    ProxyAuthenticationRequired = 407,

    RequestTimeout = 408,

    Conflict = 409,

    Gone = 410,

    LengthRequired = 411,

    PreconditionFailed = 412,

    RequestEntityTooLarge = 413,

    RequestUriTooLong = 414,

    UnsupportedMediaType = 415,

    RequestedRangeNotSatisfiable = 416,

    ExpectationFailed = 417,

    MisdirectedRequest = 421,

    UnprocessableContent = 422,

    Locked = 423,

    FailedDependency = 424,

    UpgradeRequired = 426,

    PreconditionRequired = 428,

    TooManyRequests = 429,

    RequestHeaderFieldsTooLarge = 431,

    UnavailableForLegalReasons = 451,

    InternalServerError = 500,

    NotImplemented = 501,

    BadGateway = 502,

    ServiceUnavailable = 503,

    GatewayTimeout = 504,

    HttpVersionNotSupported = 505,

    VariantAlsoNegotiates = 506,

    InsufficientStorage = 507,

    LoopDetected = 508,

    NotExtended = 510,

    NetworkAuthenticationRequired = 511
}