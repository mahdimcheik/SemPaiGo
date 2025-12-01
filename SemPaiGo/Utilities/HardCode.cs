namespace SemPaiGo.Utilities;

public static class HardCode
{
    // Roles
    public static Guid ROLE_SUPER_ADMIN => Guid.Parse("bde5556b-562d-431f-9ff9-d31a5f5cb8c5");
    public static Guid ROLE_ADMIN => Guid.Parse("4a5eaf2f-0496-4035-a4b7-9210da39501c");
    public static Guid ROLE_TEACHER => Guid.Parse("87a0a5ed-c7bb-4394-a163-7ed7560b3703");
    public static Guid ROLE_STUDENT => Guid.Parse("87a0a5ed-c7bb-4394-a163-7ed7560b4a01");

    // Genders
    public static Guid GENDER_MALE => Guid.Parse("1de5556b-562d-431f-9ff9-d31a5f5cb8c5");
    public static Guid GENDER_FEMALE => Guid.Parse("2a5eaf2f-0496-4035-a4b7-9210da39501c");
    public static Guid GENDER_OTHER => Guid.Parse("3ba0a5ed-c7bb-4394-a163-7ed7560b3703");

}