interface ErrorResponse {
    statusCode: number;
    errorMessage: string;
    description?: string;
}

export default ErrorResponse;