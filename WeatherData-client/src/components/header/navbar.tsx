import {
    Typography
} from "@material-tailwind/react";
import navbarItems from "./const/navbarItems";

const NavbarItems: React.FC = () => {
    return (
        <>
            <ul className="mb-4 mt-2 flex flex-col gap-2 lg:mb-0 lg:mt-0 lg:flex-row lg:items-center lg:gap-6">
                {navbarItems.map(item => (
                    <Typography
                        key={item.id}
                        as="li"
                        variant="small"
                        color="blue-gray"
                        className="p-1 font-normal"
                    >
                        <a href={item.route} className="flex items-center">
                            {item.label}
                        </a>
                    </Typography>
                ))}
            </ul>
        </>
    );
}

export default NavbarItems;