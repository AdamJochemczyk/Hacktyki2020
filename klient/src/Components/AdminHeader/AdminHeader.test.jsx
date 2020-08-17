import React from "react";
import AdminHeader from "./AdminHeader";
import { shallow } from "enzyme";
import { render, screen, fireEvent } from "@testing-library/react";

describe("Tests for AdminHeader", () => {

  test("AdminHeader renders correctly", () => {
    const wrapper = shallow(<AdminHeader />);
    expect(wrapper).toMatchSnapshot();
  });

  test("link have good routes", () => {
    render(<AdminHeader />);
    expect(screen.getByText("Reserve car").closest("a").href).toBe(
      "http://localhost/reserve-car"
    );
    expect(screen.getByText("Reservations").closest("a").href).toBe(
      "http://localhost/admin/user-history"
    );
    fireEvent.click(screen.getByText("Add"));
    expect(screen.getByText("Car").closest("a").href).toBe(
      "http://localhost/admin/add-car"
    );
    expect(screen.getByText("User").closest("a").href).toBe(
      "http://localhost/admin/add-user"
    );
    fireEvent.click(screen.getByText("Manager tables"));
    expect(screen.getByText("Users").closest("a").href).toBe(
      "http://localhost/admin/user-manager"
    );
    expect(screen.getByText("Cars").closest("a").href).toBe(
      "http://localhost/admin/car-manager"
    );
    expect(screen.getByText("History").closest("a").href).toBe(
      "http://localhost/admin/admin-history"
    );
    expect(screen.getByText("Defects").closest("a").href).toBe(
      "http://localhost/admin/defects-manager"
    );
  });
});
