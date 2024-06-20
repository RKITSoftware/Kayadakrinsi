export const collegesH = [
    {
        id: "1",
        text: "College of Engineering",
        items: [
            {
                id: "1_1",
                text: "Computer Science",
                items: [
                    { id: "1_1_1", text: "Artificial Intelligence", seats: 30, available: true },
                    { id: "1_1_2", text: "Data Science", seats: 25, available: false },
                    { id: "1_1_3", text: "Cybersecurity", seats: 20, available: true }
                ]
            },
            {
                id: "1_2",
                text: "Electrical Engineering",
                items: [
                    { id: "1_2_1", text: "Power Systems", seats: 15, available: true },
                    { id: "1_2_2", text: "Control Systems", seats: 10, available: false },
                    { id: "1_2_3", text: "Electronics", seats: 20, available: true }
                ]
            }
        ]
    },
    {
        id: "2",
        text: "College of Arts and Sciences",
        items: [
            {
                id: "2_1",
                text: "Mathematics",
                items: [
                    { id: "2_1_1", text: "Algebra", seats: 40, available: true },
                    { id: "2_1_2", text: "Geometry", seats: 35, available: true },
                    { id: "2_1_3", text: "Calculus", seats: 30, available: false }
                ]
            },
            {
                id: "2_2",
                text: "Physics",
                items: [
                    { id: "2_2_1", text: "Classical Mechanics", seats: 25, available: true },
                    { id: "2_2_2", text: "Quantum Physics", seats: 20, available: true },
                    { id: "2_2_3", text: "Thermodynamics", seats: 15, available: false }
                ]
            }
        ]
    }
];


export const colleges = [
    { id: 1, parentId: 0, text: "College of Engineering" },
    { id: 2, parentId: 1, text: "Computer Science", category: "Engineering" },
    { id: 3, parentId: 2, text: "Artificial Intelligence", category: "Computer Science", seats: 30, available: true },
    { id: 4, parentId: 2, text: "Data Science", category: "Computer Science", seats: 25, available: false },
    { id: 5, parentId: 2, text: "Cybersecurity", category: "Computer Science", seats: 20, available: true },
    { id: 6, parentId: 1, text: "Electrical Engineering", category: "Engineering" },
    { id: 7, parentId: 6, text: "Power Systems", category: "Electrical Engineering", seats: 15, available: true },
    { id: 8, parentId: 6, text: "Control Systems", category: "Electrical Engineering", seats: 10, available: false },
    { id: 9, parentId: 6, text: "Electronics", category: "Electrical Engineering", seats: 20, available: true },
    { id: 10, parentId: 0, text: "College of Arts and Sciences" },
    { id: 11, parentId: 10, text: "Mathematics", category: "Arts and Sciences" },
    { id: 12, parentId: 11, text: "Algebra", category: "Mathematics", seats: 40, available: true },
    { id: 13, parentId: 11, text: "Geometry", category: "Mathematics", seats: 35, available: true },
    { id: 14, parentId: 11, text: "Calculus", category: "Mathematics", seats: 30, available: false },
    { id: 15, parentId: 10, text: "Physics", category: "Arts and Sciences" },
    { id: 16, parentId: 15, text: "Classical Mechanics", category: "Physics", seats: 25, available: true },
    { id: 17, parentId: 15, text: "Quantum Physics", category: "Physics", seats: 20, available: true },
    { id: 18, parentId: 15, text: "Thermodynamics", category: "Physics", seats: 15, available: false }
];
