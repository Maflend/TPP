namespace TPP.Api.Endpoints.Users.GetUsers;

public class GetUsersRequest
{
    public OrderTypeEnum OrderType { get; set; } = OrderTypeEnum.None;
}

public enum OrderTypeEnum
{
    None = 0,
    ByOnline = 1,
    ByPositive = 2,
    ByNegative = 3
}