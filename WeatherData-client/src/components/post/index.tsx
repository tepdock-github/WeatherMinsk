import {
    Card,
    CardBody,
    Typography,
    Button
} from "@material-tailwind/react";
import Post from "../../entities/post";
import { Link } from 'react-router-dom';

const PostCard: React.FC<Post> = ({
  id,
  city,
  country,
  date,
  condition,
  temperatureCelsius,
  temperatureFahrenheit
}) => {
    return (
        <Card className="mt-6 w-full min-w-fit max-w-xs mx-auto">
            <CardBody>
                <Typography variant="h5" color="blue-gray" className="mb-2">
                    {condition} ({temperatureCelsius}°C/{temperatureFahrenheit}°F)
                </Typography>
                <Typography variant="h6" color="blue-gray" className="mb-2">
                    {date} - {city}/{country}
                </Typography>
                <Link to={`/posts/${id}`}>
                      <Button>More</Button>  
                </Link>
            </CardBody>
        </Card>
    );
}

export default PostCard;