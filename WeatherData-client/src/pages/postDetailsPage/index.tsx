import { useEffect, useState } from "react";
import { fetchPostById } from "../../API/WeatherDataAPI";
import Header from "../../components/header";
import PostCardDeatils from "../../components/postDetails";
import Post from "../../entities/post";
import { useParams } from "react-router-dom";
import ErrorResponse from "../../entities/errorResponse";
import ErrorComponent from "../../components/error";

const PostDetailsPage: React.FC = () => {
    const { id } = useParams();
    const [post, setPost] = useState<Post | undefined>();
    const [error, setError] = useState<ErrorResponse | null>(null);

    useEffect(() => {
        const fetchData = async () => {
          try {
            if (id !== undefined) {
              const postId = parseInt(id);
              const post = await fetchPostById(postId);
              setPost(post);
            } else setError({ statusCode: 400, errorMessage: "ID is undefined" });

          } catch (error: any) {
            setError({ statusCode: error.statusCode, errorMessage: error.errorMessage });
          }
        };
      
        fetchData();
      }, [id]);
      

    return (
        <>
            <Header />
            <div className="w-full flex items-center flex-col">
                <div className="w-full max-w-screen-xl flex flex-wrap">
                    {error ? (
                        <ErrorComponent statusCode={error.statusCode} errorMessage={error.errorMessage} />
                    ) : post ? (
                        <PostCardDeatils
                            id={post.id}
                            city={post.city}
                            date={post.date}
                            condition={post.condition}
                            country={post.country}
                            humidity={post.humidity}
                            cloud={post.cloud}
                            temperatureCelsius={post.temperatureCelsius}
                            temperatureFahrenheit={post.temperatureFahrenheit}
                        />
                    ) : (
                        <div className="w-full text-center text-3xl mt-4">Loading...</div>
                    )}
                </div>
            </div>
        </>
    );
}

export default PostDetailsPage;