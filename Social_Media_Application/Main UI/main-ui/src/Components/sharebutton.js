import { useState } from "react";

function ShareButton({ postId }) {
  const [copied, setCopied] = useState(false);
  const postUrl = `${window.location.origin}/post/${postId}`;

  const handleShare = async () => {
    try {
      await navigator.clipboard.writeText(postUrl);
      setCopied(true);
      setTimeout(() => setCopied(false), 2000); // Reset after 2 sec
    } catch (err) {
      console.error("Failed to copy:", err);
    }
  };

  return (
    <button onClick={handleShare}>
      {copied ? "Link Copied!" : "Share"}
    </button>
  );
}
export default ShareButton;