import { useEffect, useState } from "react";
import { fetchAllPosts } from "../../API/WeatherDataAPI";
import Header from "../../components/header";
import PostCard from "../../components/post";
import Post from "../../entities/post";
import ErrorResponse from "../../entities/errorResponse";
import ErrorComponent from "../../components/error";

const HomePage: React.FC = () => {
    const [posts, setPosts] = useState<Post[]>([]);
    const [error, setError] = useState<ErrorResponse | null>(null);

    useEffect(() => {
        const fetchData = async () => {
          try {
            const response = await fetchAllPosts();
            setPosts(response);
          } catch (error: any) {
            setError({ statusCode: error.statusCode, errorMessage: error.errorMessage });
          }
        };
        fetchData();
      }, []);

      return (
        <>
          <Header />
      
          <div className="w-full flex items-center flex-col">
            <div className="w-full max-w-screen-xl flex flex-wrap">
              {error ? (
                <ErrorComponent statusCode={error.statusCode} errorMessage={error.errorMessage} />
              ) : (
                posts.map((item) => (
                  <PostCard
                    key={item.id}
                    id={item.id}
                    city={item.city}
                    date={item.date}
                    condition={item.condition}
                    country={item.country}
                    humidity={item.humidity}
                    cloud={item.cloud}
                    temperatureCelsius={item.temperatureCelsius}
                    temperatureFahrenheit={item.temperatureFahrenheit}
                  />
                ))
              )}
            </div>
          </div>
        </>
      );
      
}

export default HomePage;