import React from "react";
import SetPassword from "./SetPassword";
import { render, screen, fireEvent, wait } from "@testing-library/react";
import { MemoryRouter } from "react-router-dom";

describe("tests for SetPassword component", () => {

    test("setpassword renders correctly", () => {
        const wrapper = render(<MemoryRouter initialEntries={["/123"]} initialIndex={1}>
        <SetPassword />
      </MemoryRouter>);
        expect(wrapper).toMatchSnapshot()
      });

  test("default value of fields is null or undefined", () => {
    const wrapper = render(<MemoryRouter initialEntries={["/123"]} initialIndex={1}>
    <SetPassword />
  </MemoryRouter>);
    expect(screen.getByText("Set password").closest('button').getAttribute("disabled")).toBe(null)
    expect(screen.getByPlaceholderText('password').nodeValue && screen.getByPlaceholderText('confirm password').nodeValue).toBe(undefined || null)
  });

  test("input validators work correctly", async ()=>{

    const wrapper = render(<MemoryRouter initialEntries={["/123"]} initialIndex={1}>
    <SetPassword />
  </MemoryRouter>);
    const firstInput=screen.getByPlaceholderText('password')
    const secondInput=screen.getByPlaceholderText('confirm password')
    fireEvent.blur(firstInput)
    await wait()
    expect(screen.getByText('Required')).not.toBe(null)
    await wait(() => {
        fireEvent.change(firstInput,{
            target: {
                value: "password"
            }
        })
      })

      fireEvent.blur(secondInput)
      expect(screen.getByText('Required')).not.toBe(null)

   await wait(()=>{
    fireEvent.change(secondInput,{
        target: {
            value: "passwords"
        }
    })
   })
   expect(screen.getByText('Passwords must match')).not.toBe(null)

   await wait(()=>{
    fireEvent.change(secondInput,{
        target: {
            value: "password"
        }
    })
   })

   expect(firstInput.value).toBe("password")
   expect(secondInput.value).toBe("password")
    
  })

  
});
