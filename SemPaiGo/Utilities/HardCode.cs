namespace SemPaiGo.Utilities;

public static class HardCode
{
    // Roles
    public static Guid ROLE_SUPER_ADMIN => Guid.Parse("bde5556b-562d-431f-9ff9-d31a5f5cb8c5");
    public static Guid ROLE_ADMIN => Guid.Parse("4a5eaf2f-0496-4035-a4b7-9210da39501c");
    public static Guid ROLE_TEACHER => Guid.Parse("87a0a5ed-c7bb-4394-a163-7ed7560b3703");
    public static Guid ROLE_STUDENT => Guid.Parse("87a0a5ed-c7bb-4394-a163-7ed7560b4a01");

    // Genders
    public static Guid GENDER_MALE => Guid.Parse("DCB8B01B-205A-4EA2-A281-004A7B1BB972");
    public static Guid GENDER_FEMALE => Guid.Parse("B68C151B-DB34-462D-A65C-90989CC96E5E");
    public static Guid GENDER_OTHER => Guid.Parse("B07B2445-F39C-4B26-8FFE-E40FE561D8BC");

    // Reservation Status
    public static Guid RESERVATION_PENDING => Guid.Parse("2EC60A91-AAB8-4753-A5D8-B131B9441E77");
    public static Guid RESERVATION_ACCEPTED => Guid.Parse ("32D854B6-6D4E-445A-9209-31A492970F2D") ;
    public static Guid RESERVATION_REJECTED => Guid.Parse("CAAB85F5-D37B-4EA0-B035-5BA3CA8DD49F")  ;
    public static Guid RESERVATION_DONE => Guid.Parse ("6D281AEC-D093-4071-8BF4-C8363361B5B4")  ;

}