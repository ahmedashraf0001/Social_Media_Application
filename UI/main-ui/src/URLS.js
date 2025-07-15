export const MainURL = "https://localhost:7242"

export const PostURL = MainURL + "/api/Post";
export const CommentsURL = MainURL + "/api/Comment/post"
export const FeedURL= PostURL + "/Feed/1/12";
export const CurrentUserURL = MainURL + "/api/User";
export const getContact = MainURL + "/api/Messaging/conversations/inbox"
export const getNotis = MainURL + "/api/Notification/Receiver"
export const searchQueryURL = MainURL + "/api/User/search?query=";
export const searchKeywordURL = MainURL + "/api/Post/search?Keyword="
export const registerURL = "https://localhost:7242/api/Auth/register"
export const loginURL = "https://localhost:7242/api/Auth/login"