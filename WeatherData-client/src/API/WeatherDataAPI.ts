import config from "../envVariables";

export const fetchAllPosts = async () => {
    const response = await fetch(`https://localhost:7246/api/v1/posts`);
    if(!response.ok)
        throw new Error(`Error: ${response.status} ${response.statusText}`);
    const posts = await response.json();
    return posts;
};
