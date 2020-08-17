import React from "react";
import UserHeader from "./UserHeader";
import { shallow } from "enzyme";
import { render, screen, fireEvent } from "@testing-library/react";

describe("Tests for UserHeader", () => {

  test("UserHeader renders correctly", () => {
    const wrapper = shallow(<UserHeader />);
    expect(wrapper).toMatchSnapshot();
  });

  test("link have good routes", () => {
    render(<UserHeader />);
    expect(screen.getByText("Reserve car").closest("a").href).toBe(
      "http://localhost/reserve-car"
    );
    expect(screen.getByText("Reservations").closest("a").href).toBe(
      "http://localhost/history"
    );
    
  });
});
