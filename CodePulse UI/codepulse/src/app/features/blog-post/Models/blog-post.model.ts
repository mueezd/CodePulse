import { category } from "../../models/category.model";

export interface BlogPost{
    id: string;
    title: string;
    shortDescription: string;
    content: string;
    featuredImageUrl: string;
    urlHandel: string;
    author: string;
    publishDate: Date;
    isVisable: boolean;
    categories: category[];
}