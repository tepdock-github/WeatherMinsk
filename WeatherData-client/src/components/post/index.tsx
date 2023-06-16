import {
    Card,
    CardBody,
    Typography,
} from "@material-tailwind/react";
import Post from "../../entities/post";

const PostCard: React.FC<Post> = ({
  city,
  country,
  condition,
  humidity,
  cloud,
  temperatureCelsius,
  temperatureFahrenheit
}) => {
    return (
        <Card className="mt-6 w-full min-w-fit max-w-xs mx-auto">
            <CardBody>
                <Typography variant="h5" color="blue-gray" className="mb-2">
                    {condition} ({temperatureCelsius}°C/{temperatureFahrenheit}°F)
                </Typography>
                <Typography>
                    In {city}({country}) humidity: {humidity} and cloud: {cloud}
                </Typography>
            </CardBody>
        </Card>
    );
}

export default PostCard;