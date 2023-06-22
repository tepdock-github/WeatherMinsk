import {
    Card,
    Input,
    Button,
    Typography
} from "@material-tailwind/react";
import { Formik, Form, Field } from 'formik';
import { Link } from 'react-router-dom';
import * as Yup from 'yup';
import { useState } from "react";
import { AddNewPost } from "../../API/WeatherDataAPI";
import { InformationCircleIcon } from "@heroicons/react/24/solid";
import ErrorResponse from "../../entities/errorResponse";
import ErrorComponent from "../../components/error";

const validationSchema = Yup.object().shape({
    city: Yup.string().required(),
    country: Yup.string().required(),
    condition: Yup.string().required(),
    clouds: Yup.number().integer().min(0).max(100).required().typeError('clouds should be a number'),
    humidity: Yup.number().integer().min(0).max(100).required().typeError('humidity should be a number'),
    temperatureCelsius: Yup.number().min(-273.15).max(100).required().typeError('temperature celsius should be a number'),
    temperatureFarenheit: Yup.number().min(-459.67).max(212).required().typeError('temperature farenheit should be a number'),
});

const initialValues = {
    city: '',
    country: '',
    condition: '',
    clouds: '',
    humidity: '',
    temperatureCelsius: '',
    temperatureFarenheit: '',
}

const AddPostForm: React.FC = () => {
    const [error, setError] = useState<ErrorResponse | null>(null);

    const handleFormSubmit = async (values: any) => {
        try {
            await AddNewPost(values);
        }  catch (error: any) {
            setError({ statusCode: error.statusCode, errorMessage: error.errorMessage });
        }
    }
    
    return (
        <div className="flex justify-center w-full">
            {error ? (
                <ErrorComponent statusCode={error.statusCode} errorMessage={error.errorMessage} />
            ) : (
                <Card color="white" shadow={true} className="mt-4 p-4">
                <Formik
                    initialValues={initialValues}
                    validationSchema={validationSchema}
                    onSubmit={handleFormSubmit}>
                    {(formikProps) => (
                        <>
                            <Typography variant="h4" color="blue-gray">
                                Add new post
                            </Typography>
                            <Typography color="gray" className="mt-1 font-normal">
                                Enter your details about weather.
                            </Typography>
                            <Form className="mt-8 mb-2 w-80 max-w-screen-lg sm:w-96">
                                <div className="mb-4 flex flex-col gap-6">
                                    <div>
                                        <Field as={Input} label="City" name="city"
                                            error={formikProps.touched.city && !!formikProps.errors.city} />
                                        {formikProps.touched.city && !!formikProps.errors.city ? <Typography variant="small" color="red" className="flex items-center gap-1 font-normal mt-2">
                                            <InformationCircleIcon className="w-4 h-4 -mt-px" />
                                            {formikProps.errors.city}
                                        </Typography> : null}
                                    </div>

                                    <div>
                                        <Field as={Input} label="Country" name="country"
                                            error={formikProps.touched.country && !!formikProps.errors.country} />
                                        {formikProps.touched.country && !!formikProps.errors.country ? <Typography variant="small" color="red" className="flex items-center gap-1 font-normal mt-2">
                                            <InformationCircleIcon className="w-4 h-4 -mt-px" />
                                            {formikProps.errors.country}
                                        </Typography> : null}
                                    </div>

                                    <div>
                                        <Field as={Input} label="Condition" name="condition"
                                            error={formikProps.touched.condition && !!formikProps.errors.condition} />
                                        {formikProps.touched.condition && !!formikProps.errors.condition ? <Typography variant="small" color="red" className="flex items-center gap-1 font-normal mt-2">
                                            <InformationCircleIcon className="w-4 h-4 -mt-px" />
                                            {formikProps.errors.condition}
                                        </Typography> : null}
                                    </div>

                                    <div>
                                        <Field as={Input} label="Humidity" name="humidity"
                                            error={formikProps.touched.humidity && !!formikProps.errors.humidity} />
                                        {formikProps.touched.humidity && !!formikProps.errors.humidity ? <Typography variant="small" color="red" className="flex items-center gap-1 font-normal mt-2">
                                            <InformationCircleIcon className="w-4 h-4 -mt-px" />
                                            {formikProps.errors.humidity}
                                        </Typography> : null}
                                    </div>

                                    <div>
                                        <Field as={Input} label="Clouds" name="clouds"
                                            error={formikProps.touched.clouds && !!formikProps.errors.clouds} />
                                        {formikProps.touched.clouds && !!formikProps.errors.clouds ? <Typography variant="small" color="red" className="flex items-center gap-1 font-normal mt-2">
                                            <InformationCircleIcon className="w-4 h-4 -mt-px" />
                                            {formikProps.errors.clouds}
                                        </Typography> : null}
                                    </div>
                                    
                                    <div>
                                        <Field as={Input} label="Temperature Celsius, °C" name="temperatureCelsius"
                                            error={formikProps.touched.temperatureCelsius && !!formikProps.errors.temperatureCelsius} />
                                        {formikProps.touched.temperatureCelsius && !!formikProps.errors.temperatureCelsius ? <Typography variant="small" color="red" className="flex items-center gap-1 font-normal mt-2">
                                            <InformationCircleIcon className="w-4 h-4 -mt-px" />
                                            {formikProps.errors.temperatureCelsius}
                                        </Typography> : null}
                                    </div>

                                    <div>
                                        <Field as={Input} label="Temperature Farenheit, °F" name="temperatureFarenheit"
                                            error={formikProps.touched.temperatureFarenheit && !!formikProps.errors.temperatureFarenheit} />
                                        {formikProps.touched.temperatureFarenheit && !!formikProps.errors.temperatureFarenheit ? <Typography variant="small" color="red" className="flex items-center gap-1 font-normal mt-2">
                                            <InformationCircleIcon className="w-4 h-4 -mt-px" />
                                            {formikProps.errors.temperatureFarenheit}
                                        </Typography> : null}
                                    </div>
                                </div>
                                <Button type="submit" disabled={!formikProps.isValid}>Submit</Button>
                                <Link to={`/`} className="ml-2">
                                    <Button>Back</Button>
                                </Link>
                            </Form>
                        </>
                    )}
                </Formik>
            </Card>
            )}
        </div>

    )
}

export default AddPostForm;