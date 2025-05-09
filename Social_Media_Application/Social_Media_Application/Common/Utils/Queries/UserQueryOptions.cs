﻿namespace Social_Media_Application.Common.Utils.Queries
{
    public class UserQueryOptions
    {
        public bool WithPosts { get; set; } = false;
        public bool WithComments { get; set; } = false;
        public bool WithLikedPosts { get; set; } = false;
        public bool WithFollowers { get; set; } = false;
        public bool WithFollowing { get; set; } = false;
    }
}
