import HomePage from "./pages/homePage"
import PostDetailsPage from "./pages/postDetailsPage";
import AddPostPage from "./pages/addPostPage";
import { BrowserRouter, Routes, Route } from 'react-router-dom';

function App() {

  return (
    <>
      <BrowserRouter>
        <Routes>
          <Route path="/" element={<HomePage/>}/>
          <Route path="/posts/:id" element={<PostDetailsPage/>}/>
          <Route path="/add-new-post" element={<AddPostPage/>}/>
        </Routes>
      </BrowserRouter>
    </>
  )
}

export default App
