const BASE_URL = "http://localhost:5023/api/v1";

export const fetchAllPosts = async () => {
    const response = await fetch(`${BASE_URL}/posts`);
    if(!response.ok) {
        const errorData = await response.json();
        throw { statusCode: response.status, errorMessage: errorData.title };
    }
    const posts = await response.json();
    return posts;
};

export const fetchPostById = async (id: number) => {
    const response = await fetch(`${BASE_URL}/posts/${id}`);
    if (!response.ok) {
        const errorData = await response.json();
        throw { statusCode: response.status, errorMessage: errorData.title };
    }
    const post = await response.json();
    return post;
}

export const AddNewPost = async (values: any) => {
    const response = await fetch(`${BASE_URL}/posts`, {
        method: 'POST',
        headers: {"Content-Type": "text/json"},
        body: JSON.stringify(values),
    })
    if (!response.ok) {
        const errorData = await response.json();
        throw { statusCode: response.status, errorMessage: errorData.title };
    }
}
