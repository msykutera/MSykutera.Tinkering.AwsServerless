﻿namespace MSykutera.Tinkering.AwsServerless.Settings
{
    public class DatabaseSettings
    {
        public const string KeyName = "Database";

        public string TableName { get; set; } = default!;
    }
}
