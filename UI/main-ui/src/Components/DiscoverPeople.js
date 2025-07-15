import { Flex } from "@chakra-ui/react";
import React from "react";
import { Link } from "react-router-dom";

const DiscoverPeople = ({ users = [], onFollow }) => {
  return (
    <div
      className="bg-white p-4"
      style={{
        textAlign: "center",
        maxWidth: "821px",
        margin: "0 auto 24px auto",
        boxShadow: "0 1px 3px rgba(0, 0, 0, 0.1)",
        border: "1px solid #e1e8ed",
        height:"130px",
        display:"flex",
        justifyContent:"center",
        alignItems:"center",
        flexDirection:"column",
        borderRadius: "16px"
      }}
    >
      <h2 className="text-lg font-semibold text-gray-800 mb-1">
        Discover People
      </h2>
      {users.length === 0 ? (
        <p className="text-sm text-gray-500">No suggestions available.</p>
      ) : (
        <ul className="space-y-4">
          {users.map((user) => (
            <li key={user.id} className="flex items-center justify-between">
              <Link
                to={`/userdetails/${user.id}`}
                className="flex items-center space-x-3 hover:bg-gray-50 p-2 rounded-md text-inherit no-underline"
              >
                <img
                  src={user.profilePictureUrl}
                  alt={`${user.firstName} ${user.lastName}`}
                  className="w-10 h-10 rounded-full object-cover border"
                />
                <div className="text-sm">
                  <p className="font-medium text-gray-900 truncate">
                    {user.firstName} {user.lastName}
                  </p>
                  <p className="text-gray-500">@{user.username}</p>
                </div>
              </Link>
              <button
                onClick={() => onFollow(user.id)}
                className="bg-blue-500 text-white text-sm px-3 py-1 rounded-md hover:bg-blue-600"
              >
                Follow
              </button>
            </li>
          ))}
        </ul>
      )}
    </div>
  );
};

export default DiscoverPeople;
