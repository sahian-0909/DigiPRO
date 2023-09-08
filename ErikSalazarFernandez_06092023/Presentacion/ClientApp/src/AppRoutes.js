import { Counter } from "./components/Counter";
import { FetchData } from "./components/FetchData";
import { Home } from "./components/Home";
import { default as Alumnos } from "./components/Alumnos";
import { default as Materias } from "./components/Materias";

const AppRoutes = [
    {
        index: true,
        element: <Home />
    },
    {
        path: '/counter',
        element: <Counter />
    },
    {
        path: '/fetch-data',
        element: <FetchData />
    },
    {
        path: '/alumnos',
        element: <Alumnos />
    },
    {
        path: '/materias',
        element: <Materias />
    }
];

export default AppRoutes;
