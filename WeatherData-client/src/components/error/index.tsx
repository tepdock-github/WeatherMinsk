import {
    Card,
    CardBody,
    Typography,
  } from "@material-tailwind/react";
import ErrorProps from "../../entities/errorResponse";
 
const ErrorComponent: React.FC<ErrorProps> = ({ statusCode, errorMessage }) => {
  return (
    <Card className="mt-6 w-full min-w-fit max-w-xs mx-auto">
      <CardBody>
        <Typography variant="h5" color="blue-gray" className="mb-2">
          {statusCode}
        </Typography>
        <Typography>{errorMessage}</Typography>
      </CardBody>
    </Card>
  );
};

export default ErrorComponent;