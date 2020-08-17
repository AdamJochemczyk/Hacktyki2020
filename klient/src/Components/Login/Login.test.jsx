import React from "react"
import Login from "./Login"
import { render, screen, fireEvent, wait } from "@testing-library/react";
import {MemoryRouter} from "react-router-dom"
import "@testing-library/jest-dom"

describe("Tests for Login component", ()=>{
    test("Login component renders correctly", ()=>{
        const wrapper = render(<MemoryRouter><Login/></MemoryRouter>)
        expect(wrapper).toMatchSnapshot()
    })
    test("default value of fields is null or undefined", () => {
        render(<MemoryRouter>
        <Login />
      </MemoryRouter>);
        expect(screen.getByText("Log in").closest('button').getAttribute("disabled")).toBe(null)
        expect(screen.getByPlaceholderText('email').nodeValue && screen.getByPlaceholderText('password').nodeValue).toBe(undefined || null)
      });
    test("input validators works correctly",async ()=>{
        render(<MemoryRouter>
            <Login />
          </MemoryRouter>);
          const mailInput=screen.getByPlaceholderText('email')
          const passwordInput=screen.getByPlaceholderText('password')
          fireEvent.blur(mailInput)
          await wait()
          expect(screen.getByText('Required'))
          await wait(()=>{
            fireEvent.change(mailInput,{
                target: {
                    value: "mail"
                }
            })
          })
          expect(screen.getByText("This isn't email")).not.toBe(null)
          await wait(()=>{
            fireEvent.change(mailInput,{
                target: {
                    value: "mail@mail.pl"
                }
            })
          })
          fireEvent.blur(passwordInput)
          await wait()
          expect(screen.getByText('Required'))
          await wait(()=>{
            fireEvent.change(passwordInput,{
                target: {
                    value: "abc"
                }
            })
          })
          expect(mailInput.value).toBe("mail@mail.pl")
          expect(passwordInput.value).toBe("abc")
          fireEvent.click(screen.getByText("Log in").closest('button'))
          await wait()
          expect(screen.getByText("Log in").closest('button').disabled).toBe(true);         
    })
})