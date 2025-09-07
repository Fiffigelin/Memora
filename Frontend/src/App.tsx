import { useEffect, useState } from 'react'
import reactLogo from './assets/react.svg'
import viteLogo from '/vite.svg'
import './App.css'
import { Client, type UserProfileDto, type UserProfileDtoApiResponse } from './api/client'
import { ConfigurationProvider } from './api/client-base'

function App() {
  const [count, setCount] = useState(0)
  const [user, setUser] = useState<UserProfileDto | null>(null);

useEffect(() => {
  const fetchUser = async () => {
    try {
      const token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiI2NGMzMjI5Zi1jM2I2LTQzNjUtOTIyOS1lMDQ2Yjg2ZWNmNWEiLCJ1bmlxdWVfbmFtZSI6IlRlc3QiLCJlbWFpbCI6InRlc3RAbWFpbC5jb20iLCJleHAiOjE3NTcyNjE3MDMsImlzcyI6Ik1lbW9yYSIsImF1ZCI6Ik1lbW9yYVVzZXJzIn0.WR0SMR0tm60C1Ex44LQa_aeLtq5BQzZnHNzcKtq5SBY";
      const config = new ConfigurationProvider(token, "http://localhost:5014");
      const client = new Client(config);
      const response = await client.userGET();

      if (response.success == true) {
        const userDto: UserProfileDto = response.data!;
        setUser(userDto);
        console.log(userDto.username);
      }
    } catch (err) {
      console.error("Fel vid hämtning av användare:", err);
    }
  };

  fetchUser();
}, []);

  return (
    <>
      <div>
        <a href="https://vite.dev" target="_blank">
          <img src={viteLogo} className="logo" alt="Vite logo" />
        </a>
        <a href="https://react.dev" target="_blank">
          <img src={reactLogo} className="logo react" alt="React logo" />
        </a>
      </div>
      <h1>Vite + React</h1>
      <div className="card">
        <button onClick={() => setCount((count) => count + 1)}>
          count is {count}
        </button>
        <p>
          Edit <code>src/App.tsx</code> and save to test HMR
        </p>
      </div>
      <p className="read-the-docs">
        Click on the Vite and React logos to learn more
      </p>
<p className="read-the-docs">
  {user
    ? `Hämtad användare: ${user.username}, ${user.email}, Listor: ${user.vocabularyLists?.map(v => v.title).join(", ")}`
    : "Laddar användare..."}
</p>
    </>
  )
}

export default App
