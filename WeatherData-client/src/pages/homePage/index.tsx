import { useEffect, useState } from "react";
import { fetchAllPosts } from "../../API/WeatherDataAPI";
import Header from "../../components/header";
import PostCard from "../../components/post";
import Post from "../../entities/post";

const HomePage: React.FC = () => {
    const [posts, setPosts] = useState<Post[]>([]);
    const [width, setWidth] = useState<number>(window.innerWidth);

    useEffect(() => {
        const fetchData = async () => {
            try {
                const response = await fetchAllPosts();
                setPosts(response);
                console.log(response)
            } catch (error) {
                //routing or setError
                console.log(error)
            }
        }
        fetchData();
    }, []);

    useEffect(() => {
        window.addEventListener("resize", () => setWidth(window.innerWidth));
    }, []);

    return (
        <>
            <Header />

            <div className="w-full flex items-center flex-col">
                <div className="w-full max-w-screen-xl flex flex-wrap">
                {posts.map(item => (
                    <PostCard
                        key={item.id}
                        id={item.id}
                        city={item.city}
                        condition={item.condition}
                        country={item.country}
                        humidity={item.humidity}
                        cloud={item.cloud}
                        temperatureCelsius={item.temperatureCelsius}
                        temperatureFahrenheit={item.temperatureFahrenheit}
                    />
                ))}
                </div>
            </div>
        </>
    );
}

export default HomePage;