﻿namespace HilbertWeb.BackendApp.Dto;

public class ManageNewsDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public DateTime Created { get; set; }
    public DateTime Updated { get; set; }
    public DateTime? Pinned { get; set; }
    public UserDto? Author { get; set; }
}
